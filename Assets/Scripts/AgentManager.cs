using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager instance;

    [SerializeField] private GameObject agent;
    [SerializeField] private int agentSpawnCount = 4;
    List<ARAgent> agents;

    void Start()
    {
        for (int i = 0; i < agentSpawnCount; i++)
            Instantiate(this.agent, this.transform.position, Quaternion.identity, this.gameObject.transform);

        this.agents = new List<ARAgent>(GetComponentsInChildren<ARAgent>());
        Debug.Log("Agents in action: " + agents.Count);
        DragReceiver receiver = GameObject.Find("DragReceiver").GetComponent<DragReceiver>();
        receiver.AgentManager = this;
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        if (hit.collider.CompareTag("Floor"))
        //        {
        //            Debug.Log("Floor hit!");
        //            this.MoveAllAgents(hit.point);
        //        }
        //    }
        //}
    }

    public void MoveAllAgents(Vector3 position)
    {
        foreach (ARAgent agent in agents)
            agent.MoveAgent(position);
    }

    public void StopAllAgents()
    {
        foreach(ARAgent agent in agents)
            agent.StopAgent();
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
}
