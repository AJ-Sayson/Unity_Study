using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private MeshRenderer objectRenderer;

    private void Start() 
    {
        objectRenderer = GetComponent<MeshRenderer>();
    }

    private void Update() 
    {
        if(gameObject.tag == "Hit" && objectRenderer.material.color != Color.red)
        {
            float changeColorSpeed = 1f * Time.deltaTime * 8f;
            objectRenderer.material.color = Color.Lerp(objectRenderer.material.color, Color.red, changeColorSpeed);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(gameObject.tag == "Untagged" && other.gameObject.tag == "Player")
        {
            gameObject.tag = "Hit";
        }
    }
}
