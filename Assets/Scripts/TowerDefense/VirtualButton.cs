using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImage))]
public class VirtualButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTrackedImageLimited;
    private ARTrackedImage m_Image;

    // Start is called before the first frame update
    void Start()
    {
        this.m_Image = GetComponent<ARTrackedImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_Image.trackingState == TrackingState.Limited)
        {
            Debug.Log($"[{this.name}] LOG: Tracked image not visible");
            this.OnTrackedImageLimited.Invoke();
        }
    }
}
