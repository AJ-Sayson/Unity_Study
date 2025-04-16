using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoIncreaseAmount = 5;
    [SerializeField] AmmoType ammoType;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Ammo>().IncreaseAmmoAmount(ammoType, ammoIncreaseAmount);
            GetComponent<Animator>().SetTrigger("OpenBox");
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject, 0.5f);
    }
}
