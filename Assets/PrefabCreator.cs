using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject cPrefab;
    [SerializeField] private GameObject bPrefab;
    [SerializeField] private Vector3 prefabOffset;

    private ARTrackedImageManager aRTrackedImageManager;
    private GameObject c;
    private GameObject b;

    private void OnEnable()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        aRTrackedImageManager.trackablesChanged.AddListener(onChanged);
    }

    private void onChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var newImage in args.added)
        {
            if (newImage.referenceImage.name == "BeeImage")
            {
                b = Instantiate(bPrefab, newImage.transform);
                b.transform.position += prefabOffset;
            }

            else if (newImage.referenceImage.name == "CloudImage")
            {
                c = Instantiate(cPrefab, newImage.transform);
                c.transform.position += prefabOffset;
            }
        }

        foreach (var updatedImage in args.updated)
        {
            // Handle updated event
        }

        foreach (var removedImage in args.removed)
        {
            if (removedImage.Value.referenceImage.name == "BeeImage" && b != null)
            {
                Destroy(b);  // Destroy the bee when its tracked image is removed
                b = null;
                bPrefab.SetActive(false);
            }
            else if (removedImage.Value.referenceImage.name == "CloudImage" && c != null)
            {
                Destroy(c);  // Destroy the cloud when its tracked image is removed  // Destroy the bee when its tracked image is removed
                c = null;
                cPrefab.SetActive(false);
            }
        }
    }
}