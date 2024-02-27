using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed = 50.0f;
    [SerializeField] private float cooldown = 1.0f;
    private Animator animator;
    private float ticks = 0.0f;
    private bool hasFired = false;

    public void Fire(bool fire)
    {
        if (this.hasFired) return;

        this.hasFired = true;
        Debug.Log($"[{this.name}] LOG: Fire set to {fire}");
        this.animator.SetBool("Fire", fire);
        GameObject projectile = Instantiate(this.bullet, this.spawnPoint.position, this.spawnPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = this.transform.forward * this.speed;
        projectile.GetComponent<Projectile>().Initialize();
    }

    void Update()
    {
        if (!this.hasFired) return;

        this.ticks += Time.deltaTime;
        if (this.ticks > cooldown)
        {
            this.ticks = 0.0f;
            this.hasFired = false;
        }
    }

    void Awake()
    {
        DamageManager.instance.RegisterTurret(this.gameObject);
        this.animator = GetComponent<Animator>();
    }
}
