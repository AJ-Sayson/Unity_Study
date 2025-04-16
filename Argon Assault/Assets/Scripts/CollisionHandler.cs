using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] GameObject masterTimeline;
    [SerializeField] GameObject modulesParentObject;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float loadDelay = 1f;
    PlayerController playerControls;

    bool systemsEnabled = false;
    int currentSceneIndex;
    int timeTillSystemsEnabled = 5;

    private void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerControls = gameObject.GetComponent<PlayerController>();

        TogglePlayerControls();
    }

    private void Update()
    {
        CheckIfEnableSystems(); // Disables Player Controls & Collisions up until a certain amount of time has passed in the timeline.
    }

    private void CheckIfEnableSystems()
    {
        if (masterTimeline.GetComponent<PlayableDirector>().time > timeTillSystemsEnabled && !systemsEnabled) 
        { 
            TogglePlayerControls();
            systemsEnabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"{this.name} ***COLLIDED WITH*** {other.gameObject.name}");

        if (!systemsEnabled || !playerControls.enabled) { return; } // Checks if the player has taken off or if the controls are already disabled.

        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        TogglePlayerControls();
        ToggleShipColliders();
        ToggleShipVisibility();

        explosionVFX.GetComponent<ParticleSystem>().Play();
        Invoke("RestartLevel", loadDelay);
    }

    private void TogglePlayerControls()
    {
        playerControls.enabled = !playerControls.enabled;
        Debug.Log($"Player Controls Enabled: {playerControls.enabled}");
    }

    private void ToggleShipColliders()
    {
        BoxCollider[] shipColliders = GetComponentsInChildren<BoxCollider>();

        foreach (BoxCollider collider in shipColliders)
        {
            collider.enabled = !collider.enabled;
        }
    }

    private void ToggleShipVisibility()
    {
        MeshRenderer[] shipModules = modulesParentObject.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer module in shipModules)
        {
            Debug.Log("Toggling visibility");
            module.enabled = !module.enabled;
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
