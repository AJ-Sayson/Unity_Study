using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;    // Initialized to this value because default value is 0.
    bool isProvoked = false;
    
    EnemyHealth enemyHealth;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        // Method(EnemyAI_OnDamageTaken) subscribing to an event(OnDamageTaken)
        enemyHealth.OnDamageTaken += EnemyAI_OnDamageTaken;
    }

    void Update()
    {
        CheckIfIsDead();

        CheckTargetDistance();
    }

    void CheckIfIsDead()
    {
        if (enemyHealth.IsDead)
        {
            this.enabled = false;

            if (!navMeshAgent.enabled) { return; }

            navMeshAgent.enabled = false;
        }
    }

    void CheckTargetDistance()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(!isProvoked && distanceToTarget > chaseRange) 
        { 
            GetComponent<Animator>().SetTrigger("idle");
            return; 
        }
        else if(isProvoked && distanceToTarget > chaseRange * 2)
        {
            isProvoked = false;

            GetComponent<Animator>().SetTrigger("idle");
            return;
        }

        EngageTarget();
    }

    void EngageTarget()
    {
        if(!navMeshAgent.enabled) { return; }
        
        FaceTarget();

        if(distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void ChaseTarget()
    {
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("move")) { return; }

        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");

        if(!navMeshAgent.enabled) { return; }

        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        if(GetComponent<Animator>().GetBool("attack")) { return; }

        GetComponent<Animator>().SetBool("attack", true);
    }

    private void EnemyAI_OnDamageTaken(object sender, System.EventArgs e)
    {
        // This method is subscribed to the OnDamageTaken event; will be called everytime the event is invoked.
        isProvoked = true;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        
        if(!isProvoked)
        {
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
        else if(isProvoked)
        {
            Gizmos.DrawWireSphere(transform.position, chaseRange * 2);
        }
    }
}
