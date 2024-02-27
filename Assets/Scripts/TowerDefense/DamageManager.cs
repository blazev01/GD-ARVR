using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TMP_Text text;

    public static DamageManager instance;

    private List<GameObject> turrets = new();

    private bool turretsRegistered = false;

    public void RegisterTurret(GameObject turret)
    {
        this.turretsRegistered = true;
        this.turrets.Add(turret);
    }

    void Update()
    {
        if (this.turretsRegistered && this.turrets.Count == 0)
            this.deathScreen.SetActive(true);

        else
        {
            for (int i = 0; i < this.turrets.Count; i++)
                if (this.turrets[i] == null) this.turrets.RemoveAt(i);

            this.text.text = this.turrets.Count.ToString();
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
}
