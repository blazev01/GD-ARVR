using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Initialize()
    {
        StartCoroutine(this.LifespanTick());
    }

    private IEnumerator LifespanTick()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Turret"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
