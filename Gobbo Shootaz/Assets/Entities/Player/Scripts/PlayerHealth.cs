using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    float playerHitPoints;

    void Start()
    {
        playerHitPoints = maxHitPoints;
    }

    public void ProcessDamage(float damage)
    {
        if(playerHitPoints <= 0) { return; }

        playerHitPoints -= damage;

        if(playerHitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandlePlayerDeath();
        }
    }
}
