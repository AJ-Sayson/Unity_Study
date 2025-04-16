using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentIndex;

    void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentIndex);

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
