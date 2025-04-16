using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 40f;
    PlayerHealth target;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if(target == null) 
        { 
            Debug.Log("ERROR: Player not found.");
            return; 
        }
        
        target.ProcessDamage(damage);
    }
}
