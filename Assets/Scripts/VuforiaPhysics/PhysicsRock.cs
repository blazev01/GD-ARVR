using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRock : MonoBehaviour
{
    [SerializeField] private float minDespawnDepth = 1.0f;
    private Transform boxTransform;
    public delegate void RockEvent();
    public RockEvent OnDeath;

    public Transform BoxTransform { set {  boxTransform = value; } }

    private void Update()
    {
        if (this.transform.position.y < this.boxTransform.position.y - this.minDespawnDepth)
        {
            this.OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
