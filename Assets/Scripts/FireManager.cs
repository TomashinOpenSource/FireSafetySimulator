using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public Transform ballon;
    public Transform[] holders;
    public GameObject foam;
    public GameObject sphere;

    public bool isWithoutSphere = false;

    private void Start()
    {
        foam.SetActive(false);
    }


    public void DropSphere()
    {
        sphere.transform.parent = null;
        sphere.AddComponent<Rigidbody>();
        isWithoutSphere = true;
        StartExtinguish();
    }
    public void StartExtinguish()
    {
        if (isWithoutSphere)
        {
            foam.SetActive(true);
        }
    }
}
