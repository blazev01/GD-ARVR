using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HeldObjectHandler : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Button deleteButton;

    private GameObject heldObject = null;

    public GameObject HeldObject
    {
        get { return this.heldObject; }
        set { this.heldObject = value; }
    }

    public void PlaceObject()
    {
        if (this.heldObject) this.heldObject = null;
    }

    public void DeleteHeldObject()
    {
        if (this.heldObject) GameObject.Destroy(this.heldObject);
    }

    private void Update()
    {
        if (this.heldObject)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            List<ARRaycastHit> hitList = new();
            if (this.raycastManager.Raycast(ray, hitList, TrackableType.PlaneEstimated) &&
                hitList.Count > 0 && hitList[0].pose.up == Vector3.up)
            {
                Vector3 position = ray.GetPoint(hitList[0].distance);
                this.heldObject.transform.position = position;
            }

            if (!this.deleteButton.interactable)
            {
                this.deleteButton.interactable = true;
            }
        }
        else
        {
            if (this.deleteButton.interactable)
            {
                this.deleteButton.interactable = false;
            }
        }
    }
}
