using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    private MeshRenderer dropperRenderer;
    private Rigidbody dropperBody;
    [SerializeField] private int timeToWait = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Caching a reference
        dropperRenderer = GetComponent<MeshRenderer>();
        dropperBody = GetComponent<Rigidbody>();

        dropperRenderer.enabled = false;
        dropperBody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Math.Floor(Time.time) == timeToWait)
        {
            // Debug.Log(timeToWait + " Seconds has elapsed");
            EnableDropper();
        }
    }

    private void EnableDropper()
    {
        dropperRenderer.enabled = true;
        dropperBody.useGravity = true;
    }
}
