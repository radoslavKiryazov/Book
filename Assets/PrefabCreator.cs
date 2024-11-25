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
    private GameObject activePrefab;

    private void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    [Obsolete]
    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    [Obsolete]
    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    [Obsolete]
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject prefab))
            {
                prefab.SetActive(false);
            }
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (spawnedPrefabs.TryGetValue(imageName, out GameObject prefab))
        {
            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                prefab.transform.position = trackedImage.transform.position + prefabOffset;

                if (!prefab.activeInHierarchy)
                {
                    prefab.SetActive(true);

                    // Special case for "bee" prefab
                    if (imageName == "bee")
                    {
                        Rigidbody beeRigidbody = prefab.GetComponent<Rigidbody>();
                        if (beeRigidbody != null)
                        {
                            beeRigidbody.isKinematic = false; // Enable physics
                        }
                    }
                }
            }
            else
            {
                // If the image is not tracked, deactivate the prefab
                prefab.SetActive(false);
            }
        }
    }
}
