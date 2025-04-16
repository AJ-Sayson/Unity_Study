using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    bool isActive = false;
    public bool IsActive { get { return isActive; } }

    void Start() 
    {
        gameOverCanvas.enabled = false;
    }

    public void HandlePlayerDeath()
    {
        isActive = true;
        gameOverCanvas.enabled = true;

        Time.timeScale = 0; // Stop time

        GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
