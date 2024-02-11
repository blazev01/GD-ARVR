using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private string[] imageNames;
    [SerializeField] private GameObject[] virtualObjPrefabsA;
    [SerializeField] private GameObject[] virtualObjPrefabsB;

    private List<GameObject> objA = new List<GameObject>();
    private List<GameObject> objB = new List<GameObject>();

    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var image in eventArgs.added)
        {
            Debug.Log("Tracked new Image: " + image.referenceImage.name);

            for (var i = 0; i < this.imageNames.Length; i++)
            {
                if (image.referenceImage.name == this.imageNames[i])
                {
                    GameObject virtualObjA = GameObject.Instantiate(virtualObjPrefabsA[i]);
                    GameObject virtualObjB = GameObject.Instantiate(virtualObjPrefabsB[i]);
                    objA.Add(virtualObjA);
                    objB.Add(virtualObjB);
                    virtualObjA.transform.position = image.transform.position;
                    virtualObjB.transform.position = image.transform.position;
                    virtualObjB.SetActive(false);
                }
            }
        }

        foreach (var image in eventArgs.updated)
        {
            //Debug.Log("Updated Image: " + image.referenceImage.name);
        }

        foreach (var image in eventArgs.removed)
        {
            //Debug.Log("Removed Image: " + image.referenceImage.name);
        }

    }

    public void ShowObjectA()
    {
        foreach (var virtualObj in this.objB)
        {
            virtualObj.SetActive(false);
        }
        foreach (var virtualObj in this.objA)
        {
            virtualObj.SetActive(true);
        }
    }
    public void ShowObjectB()
    {
        foreach (var virtualObj in this.objA)
        {
            virtualObj.SetActive(false);
        }
        foreach (var virtualObj in this.objB)
        {
            virtualObj.SetActive(true);
        }
    }
}
