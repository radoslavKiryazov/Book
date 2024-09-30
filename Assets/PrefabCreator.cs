using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject beePrefab;
    [SerializeField] private Vector3 prefabOffset;

    private ARTrackedImageManager aRTrackedImageManager;
    private GameObject bee;

    private void OnEnable()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        aRTrackedImageManager.trackablesChanged.AddListener(onChanged);
    }

    private void onChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var newImage in args.added)
        {
            bee = Instantiate(beePrefab, newImage.transform);
            bee.transform.position += prefabOffset;
        }

        foreach (var updatedImage in args.updated)
        {
            // Handle updated event
        }

        foreach (var removedImage in args.removed)
        {
            // Handle removed event
        }
    }
}
