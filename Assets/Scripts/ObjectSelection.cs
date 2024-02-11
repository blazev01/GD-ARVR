using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs;

    private int selectedIndex = 0;

    public int SelectedIndex
    {
        set { this.selectedIndex = value; }
    }

    public GameObject GetSelectedObject()
    {
        return this.objectPrefabs[selectedIndex];
    }

    public void DeleteAllObjects()
    {
        GameObject[] moveables = GameObject.FindGameObjectsWithTag("Moveable");

        for (int i = 0; i < moveables.Length; i++)
        {
            GameObject.Destroy(moveables[i]);
        }
    }
}
