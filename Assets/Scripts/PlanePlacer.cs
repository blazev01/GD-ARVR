using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanePlacer : MonoBehaviour
{
    private ARRaycastManager raycastManager;

    [SerializeField] GameObject prefabToPlane;

    void Start()
    {
        this.raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount != 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began &&
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            GameObject hitObject = null;
            List<ARRaycastHit> hitList = new();

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                hitObject = hit.collider.gameObject;
            }

            if (hitObject != null && !hitObject.TryGetComponent<ARPlane>(out _))
            {
                Debug.Log("Object hit.");
                GameObject.Destroy(hitObject.gameObject);
            }
            else if (this.raycastManager.Raycast(ray, hitList, TrackableType.PlaneEstimated))
            {
                if (hitList.Count > 0 && hitList[0].pose.up == Vector3.up)
                {
                    Debug.Log("Horizontal plane detected.");
                    Vector3 spawnPos = ray.GetPoint(hitList[0].distance);
                    this.PlaceObject(spawnPos);
                }
                else
                {
                    Debug.Log("No horizontal plane detected.");
                }
            }
        }
    }

    private void PlaceObject(Vector3 worldPos)
    {
        GameObject obj = GameObject.Instantiate(prefabToPlane);
        obj.transform.position = worldPos;
    }
}
