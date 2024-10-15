using System;
using System.Collections.Generic;
using UnityEngine;
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
            newPrefab.SetActive(false); // Set prefabs inactive initially
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    [Obsolete]
    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += onChanged;
    }

    [Obsolete]
    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= onChanged;
    }

    [Obsolete]
    private void onChanged(ARTrackedImagesChangedEventArgs args)
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
            spawnedPrefabs[removedImage.referenceImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage newImage)
    {
        string name = newImage.referenceImage.name;
        Vector3 position = newImage.transform.position;

        if (spawnedPrefabs.TryGetValue(name, out GameObject prefab))
        {
            prefab.transform.position = position;
            prefab.SetActive(true);

            foreach (GameObject go in spawnedPrefabs.Values)
            {
                if (go != prefab)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
