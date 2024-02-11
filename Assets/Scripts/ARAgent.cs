using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ARAgent : MonoBehaviour
{
    NavMeshAgent agent;

    public void MoveAgent(Vector3 position)
    {
        agent.isStopped = false;
        agent.destination = position;
        Debug.Log("Agent is moving.");
    }

    public void StopAgent()
    {
        agent.isStopped = true;
        Debug.Log("Agent stopped.");
    }

    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();
    }
}
