using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxhitPoints = 5;
    
    [Tooltip("Adds amount to maxHitPoints when enemy dies.")] 
    [SerializeField] int difficultyRamp = 1;

    int currentHitPoints = 0;

    Enemy enemyScript;

    void OnEnable() 
    {
        currentHitPoints = maxhitPoints;
    }

    void Start() {
        enemyScript = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints -= 1;

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxhitPoints += difficultyRamp;
            enemyScript.rewardGold();
        }
    }
}
