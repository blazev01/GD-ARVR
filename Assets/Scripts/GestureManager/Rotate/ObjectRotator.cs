using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour, IRotateable
{
    [SerializeField, Range(0.0f, 2.0f)]
    private float rotationSpeed = 1.0f;

    public void OnRotate(RotateEventArgs args)
    {
        this.transform.rotation.ToAngleAxis(out float currentAngle, out _);
        this.transform.rotation = Quaternion.AngleAxis((-this.rotationSpeed * args.Angle) + currentAngle, Vector3.up);
    }
}
