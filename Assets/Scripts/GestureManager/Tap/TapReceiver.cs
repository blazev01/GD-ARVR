using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapReceiver : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    private ObjectSelection selection;
    private HeldObjectHandler objectHandler;

    private void OnTap(object Sender, TapEventArgs args)
    {
        if (args.HitObject == null) return;

        if (this.objectHandler.HeldObject)
        {
            this.objectHandler.PlaceObject();
            Debug.Log("Object placed.");
        }

        else if (args.HitObject.CompareTag("Moveable"))
        {
            this.objectHandler.HeldObject = args.HitObject;
            Debug.Log("Holding object.");
        }
        else if (args.HitObject.TryGetComponent<ARPlane>(out _))
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
                    GameObject obj = GameObject.Instantiate(this.selection.GetSelectedObject());
                    obj.transform.position = spawnPos;
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
        this.selection = GetComponent<ObjectSelection>();
        this.objectHandler = GetComponent<HeldObjectHandler>();
        GestureManager.Instance.OnTap += this.OnTap;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTap -= this.OnTap;
    }
}
