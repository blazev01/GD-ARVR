using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageHandlerV2 : MonoBehaviour
{
    [SerializeField] private string imageName;
    [SerializeField] private GameObject beaconPrefab;
    [SerializeField] private GameObject visibilityText;

    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var image in eventArgs.added)
        {
            Debug.Log("Tracked new Image: " + image.referenceImage.name);

            if (image.referenceImage.name == this.imageName)
            {
                GameObject obj = Instantiate(this.beaconPrefab);
                obj.transform.position = image.transform.position;
            }
        }

        foreach (var image in eventArgs.updated)
        {
            if (image.trackingState == TrackingState.Limited)
            {
                this.visibilityText.SetActive(true);
                if (AgentManager.instance != null)
                    AgentManager.instance.StopAllAgents();
            }
            else this.visibilityText.SetActive(false);

            //Debug.Log("Updated Image: " + image.referenceImage.name);
        }

        foreach (var image in eventArgs.removed)
        {
            Debug.Log("Removed Image: " + image.referenceImage.name);
        }

    }
}
