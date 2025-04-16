using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float xrotateValue = 0;
    [SerializeField] private float yrotateValue = 0;
    [SerializeField] private float zrotateValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float frameSpeed = 1f * Time.deltaTime;
        transform.Rotate(frameSpeed * xrotateValue, frameSpeed * yrotateValue, frameSpeed * zrotateValue);
    }
}
