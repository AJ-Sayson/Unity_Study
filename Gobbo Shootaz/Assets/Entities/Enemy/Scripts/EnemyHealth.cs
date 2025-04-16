using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    GameObject parentObject;
    float hitPoints;
    bool isDead = false;
    public bool IsDead { get { return isDead; } }

    void OnEnable() {
        hitPoints = maxHitPoints;
    }

    // create a public method that reduces hp by param damage

    public void ProcessDamage(float damageValue)
    {
        if(hitPoints <= 0) { return; }

        hitPoints -= damageValue;

        //OnDamageTaken Event
        OnDamageTaken?.Invoke(this, null);

        if(hitPoints <= 0)
        {
            ProcessDeath();
        }
    }

    void ProcessDeath()
    {
        isDead = true;
        GetComponent<Animator>().SetTrigger("death");
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public event EventHandler OnDamageTaken;

    // Study Notes - If you want the eventhandler to pass custom data instead of null
    //
    // You first have to create a data class inheriting from EventArgs (Remember to define the properties here accordingly
    //
    // For example:
    //     public class CustomDataClass : EventArgs
    // {
    //     public float Property { get; set; }
    // }
    //
    // You then define an EventHandler<ClassNameOfTheDataClass>
    //
    // Instantiate the data class and provide the necessary values to its properties
    //
    // For example:
    //
    // CustomDataClass.Property = value;
    //
    // You then use the data class as the 2nd parameter when invoking the eventhandler
    // Congrats, you have now passed the custom data to all subscribed methods for the event.
}
