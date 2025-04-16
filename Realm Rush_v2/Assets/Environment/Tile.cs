using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;

    // Property
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Pathfinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinder>();

        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown() {
        Node node = gridManager.GetNode(coordinates);

        if(node == null) { return; } 

        if(!node.isWalkable || pathFinder.WillBlockPathOnPlacement(coordinates)) { return; }

        bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

        if(!isSuccessful) { return; }

        gridManager.BlockNode(coordinates);
        pathFinder.NotifyReceivers();
    }
}
