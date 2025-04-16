using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;

    // Property
    public bool IsPlaceable { get { return isPlaceable; } }

    // Getter method
    // public bool GetIsPlaceable()
    // {
    //     return isPlaceable;
    // }

    void OnMouseDown() {
        if(!isPlaceable) { return; }

        bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
        isPlaceable = !isPlaced;
    }
}
