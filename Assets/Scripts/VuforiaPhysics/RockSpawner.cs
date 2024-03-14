using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rockPRefab;
    [SerializeField] private int maxRocks = 5;

    private int rocks = 0;

    private void SpawnRock()
    {
        GameObject rock = Instantiate(rockPRefab, this.transform.position, Quaternion.identity, this.transform);
        rock.GetComponent<PhysicsRock>().BoxTransform = this.transform.parent;
        rock.GetComponent<PhysicsRock>().OnDeath += () => { this.rocks--; };
        this.rocks++;
        Debug.Log("Spawned Rock");
    }

    void Update()
    {
        if (this.rocks < maxRocks)
            this.SpawnRock();
    }

    void Start()
    {
        for (int i = 0; i < this.maxRocks; i++)
            this.SpawnRock();
    }
}
