using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private bool start = false;

    private IEnumerator Spawn()
    {
        while (true)
        {
            int randIndex = Random.Range(0, spawnPoints.Length);

            if (randIndex < spawnPoints.Length)
            {
                Transform spawnPoint = this.spawnPoints[randIndex];

                GameObject enemy = Instantiate(this.enemy, spawnPoint.position, Quaternion.identity);
                enemy.GetComponent<Rigidbody>().velocity = this.transform.forward * this.speed;
                enemy.GetComponent<Enemy>().Initialize();
            }
            
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void Update()
    {
        if (this.start)
        {
            this.start = false;
            StartCoroutine(this.Spawn());
        }
    }
}
