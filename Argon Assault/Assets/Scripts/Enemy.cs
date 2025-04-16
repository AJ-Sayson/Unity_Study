using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scoreValue = 1;
    [SerializeField] int healthPoints = 2;

    ScoreBoard scoreBoard;
    GameObject VFXParentContainer;

    private void Start() 
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();                                // Don't use FindObjectOfType in update. It's okay in scenarios like this (even when this enemy would be instantiated on runtime).
        VFXParentContainer = GameObject.FindWithTag("SpawnAtRuntime");
    }

    private void OnParticleCollision(GameObject other)
    {
        Vector3 playerMissilePos = other.gameObject.transform.position;

        ProcessOnHit(playerMissilePos);

        if(healthPoints > 0) { return; }

        StartDeathSequence();
    }

    private void ProcessOnHit(Vector3 playerMissilePos)
    {
        InstantiateVFX(hitVFX, playerMissilePos);
        healthPoints -= 1;

        ChangeMeshColor();
    }

    private void StartDeathSequence()
    {
        InstantiateVFX(deathFX, transform.position); 
        scoreBoard.IncreaseScore(scoreValue);
        Destroy(gameObject);
    }

    private void InstantiateVFX(GameObject VFXToSpawn, Vector3 position)
    {
        GameObject fx = Instantiate(VFXToSpawn, position, Quaternion.identity);
        fx.transform.parent = VFXParentContainer.transform;                        // Parents the instantiated object to the parent object specified.
    }

    private void ChangeMeshColor()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        Invoke("ResetMeshColor", 0.1f);
    }

    private void ResetMeshColor()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
