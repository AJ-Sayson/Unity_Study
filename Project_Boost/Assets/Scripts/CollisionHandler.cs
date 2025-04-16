using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Parameters - for tuning, typically set in the editor
    [SerializeField] bool debugEnabled = false;
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioSource onSuccessSFX;
    [SerializeField] AudioSource onCrashSFX;
    [SerializeField] ParticleSystem onSuccessVFX;
    [SerializeField] ParticleSystem onCrashVFX;

    // Cache - e.g. references for readability or speed.

    // State - private instance (member) variables.
    int currentSceneIndex = 0;
    bool isTransitioning = false;
    bool dontCheckCollisions = false;
    

    void Start() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        CheckForDebugKeys();
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || dontCheckCollisions) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void CheckForDebugKeys()
    {
        if(!debugEnabled) { return; }

        if (Input.GetKeyDown(KeyCode.L))
        {
            DisablePlayerControls();
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            dontCheckCollisions = !dontCheckCollisions;
        }
    }

    void StartSuccessSequence()
    {
        DisablePlayerControls();

        onSuccessVFX.Play();
        onSuccessSFX.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        DisablePlayerControls();

        onCrashVFX.Play();
        onCrashSFX.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void DisablePlayerControls()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = ++currentSceneIndex;

        // Resets the level to the first one if current level is the last one
        if(nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
