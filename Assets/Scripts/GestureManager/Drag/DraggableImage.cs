using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableImage : MonoBehaviour, IDraggable
{
    public void OnDrag(DragEventArgs args)
    {
        Debug.Log("Dragging image");
        Ray ray = Camera.main.ScreenPointToRay(args.TargetFinger.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
            this.transform.position = ray.GetPoint(hit.distance);
    }
}
