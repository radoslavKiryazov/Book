using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] placeablePrefabs;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    [SerializeField] private Vector3 prefabOffset;

    private ARTrackedImageManager aRTrackedImageManager;


    private void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }


    }
    private void OnEnable()
    {
        aRTrackedImageManager.trackablesChanged.AddListener(onChanged);
    }
    private void OnDisable()
    {
        aRTrackedImageManager?.trackablesChanged.RemoveListener(onChanged);
    }

    private void onChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage newImage in args.added)
        {
            UpdateImage(newImage);
            
        }

        foreach (ARTrackedImage updatedImage in args.updated)
        {
            UpdateImage(updatedImage);
        }

        foreach (var removedImage in args.removed)
        {
            Console.WriteLine(removedImage);
            spawnedPrefabs[removedImage.Value.name].SetActive(false);
        }
    }
    private void UpdateImage(ARTrackedImage newImage)
    {
        string name = newImage.referenceImage.name;
        Vector3 position = newImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach(GameObject gameObject in spawnedPrefabs.Values)
        {
            if(gameObject.name != name)
            {
                gameObject.SetActive(false);
            }
        }
    }
}