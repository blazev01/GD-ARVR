using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneVisualizer : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private bool visualizePlanes = true;

    public bool VisualizePlanes
    {
        get { return this.visualizePlanes; }
        set { this.visualizePlanes = value; }
    }

    private void Update()
    {
        foreach (var trackable in this.planeManager.trackables)
        {
            trackable.GetComponent<ARPlaneMeshVisualizer>().enabled = visualizePlanes;
        }
        
    }

    private void Start()
    {
        this.planeManager = GetComponent<ARPlaneManager>();
    }
}
