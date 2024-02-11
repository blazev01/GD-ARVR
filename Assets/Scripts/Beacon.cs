using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    private AgentManager agentManager;
    public AgentManager AgentManager
    {
        set { this.agentManager = value; }
    }

    void Update()
    {
        if (this.agentManager != null)
            this.agentManager.MoveAllAgents(this.transform.position);
    }
}
