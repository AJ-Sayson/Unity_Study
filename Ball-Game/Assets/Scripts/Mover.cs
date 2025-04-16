using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Speed Value for player movement -- taken from Unity's Input Manager
        float x_Value = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float z_Value = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(x_Value,0f,z_Value);
    }
}
