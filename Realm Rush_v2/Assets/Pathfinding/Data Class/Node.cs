using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is an example of a pure c# data class
// pure c# classes cannot be added to gameObjects in Unity.

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    // Constructor
    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
