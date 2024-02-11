using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject levelPrefab;

    private GameObject levelInstance;

    private void OnTap(object Sender, TapEventArgs args)
    {
        if (args.HitObject == null) return;

        if (this.levelInstance == null &&
            args.HitObject.TryGetComponent<ARPlane>(out _))
        {
            Ray ray = Camera.main.ScreenPointToRay(args.TapPosition);
            List<ARRaycastHit> hitList = new();
            if (this.raycastManager.Raycast(ray, hitList, TrackableType.PlaneEstimated) &&
                hitList.Count > 0)
            {
                if (hitList[0].pose.up == Vector3.up)
                {
                    Debug.Log("Horizontal plane detected.");
                    Vector3 spawnPos = ray.GetPoint(hitList[0].distance);
                    this.levelInstance = GameObject.Instantiate(this.levelPrefab);
                    this.levelInstance.transform.position = spawnPos;
                }
                else
                {
                    Debug.Log("Other plane detected.");
                }
            }
        }
    }

    void Start()
    {
        GestureManager.Instance.OnTap += this.OnTap;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTap -= this.OnTap;
    }
}
