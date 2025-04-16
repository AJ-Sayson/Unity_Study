using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] // Use this tag with caution: it'll make the script run both in edit & play mode.
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();

        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    void  DisplayCoordinates()
    {
        // Do not use anything associated with the Unity Editor for the final build (they are not included when building the game).
        // Instead, when building the game, put scripts like this in the Editor folder (The spelling is important. The Editor folder gets ignored during the build process)
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); // We get the z-coordinate instead (we use the top-down perspective of the 3D space for our 2D coordinates reference).

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void SetLabelColor()
    {
        if(waypoint.IsPlaceable && label.color == blockedColor)
        {
            label.color = defaultColor;
        }
        else if(!waypoint.IsPlaceable && label.color == defaultColor)
        {
            label.color = blockedColor;
        }
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
}
