using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class AnchorPlacer : MonoBehaviour
{
    ARAnchorManager manager;

    [SerializeField] private GameObject prefabToAnchor;
    [SerializeField] private float forwardOffset = 2.0f;

    public float ForwardOffset
    {
        get { return this.forwardOffset; }
        set {  this.forwardOffset = value; }
    }

    void Start()
    {
        manager = GetComponent<ARAnchorManager>();
    }

    void Update()
    {
        if (Input.touchCount != 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began &&
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            GameObject hitObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                hitObject = hit.collider.gameObject;
            }

            if (hitObject != null)
            {
                Debug.Log("Object hit.");
                GameObject.Destroy(hitObject.transform.parent.gameObject);
            }
            else
            {
                Debug.Log("No object hit.");
                Vector3 spawnPos = ray.GetPoint(this.forwardOffset);
                this.AnchorObject(spawnPos);
            }
        }
    }

    private void AnchorObject(Vector3 worldPos)
    {
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = worldPos;
        newAnchor.AddComponent<ARAnchor>();

        GameObject obj = GameObject.Instantiate(prefabToAnchor, newAnchor.transform);
        obj.transform.localPosition = Vector3.zero;
    }
}
