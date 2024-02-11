using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconTracker : MonoBehaviour
{
    [SerializeField] private AgentManager agentManager;
    private GameObject beacon;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null &&
            collision.gameObject.CompareTag("Moveable"))
        {
            this.beacon = collision.gameObject;
            Debug.Log("Beacon found!");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != null &&
            collision.gameObject ==  this.beacon)
            this.beacon = null;
    }

    void Update()
    {
        if (beacon != null)
            this.agentManager.MoveAllAgents(this.beacon.transform.position);
    }
}
