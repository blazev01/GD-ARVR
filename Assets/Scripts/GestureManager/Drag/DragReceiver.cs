using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DragReceiver : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    private AgentManager agentManager;
    public AgentManager AgentManager { set { agentManager = value; } }

    private void OnDrag(object sender, DragEventArgs args)
    {
        if (args.HitObject != null && args.HitObject.CompareTag("Moveable"))
        {
            Ray ray = Camera.main.ScreenPointToRay(args.TargetFinger.position);
            List<ARRaycastHit> hitList = new();
            if (this.raycastManager.Raycast(ray, hitList, TrackableType.PlaneEstimated) &&
                hitList.Count > 0)
            {
                if (hitList[0].pose.up == Vector3.up)
                {
                    Debug.Log("Horizontal plane detected.");
                    Vector3 position = ray.GetPoint(hitList[0].distance);
                    args.HitObject.transform.position = position;
                    if (this.agentManager != null) this.agentManager.MoveAllAgents(position);
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
        GestureManager.Instance.OnDrag += OnDrag;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnDrag -= OnDrag;
    }

    void Update()
    {
        
    }
}
