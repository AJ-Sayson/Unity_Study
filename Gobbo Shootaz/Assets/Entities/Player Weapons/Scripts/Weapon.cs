using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float weaponDamage = 30f;
    [SerializeField] float cooldownBetweenShots = 0.5f;
    [SerializeField] AmmoType ammoType;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitImpactEffect;

    Ammo ammoSlot;
    float remainingCooldown;
    DeathHandler deathHandler;

    void Start()
    {
        ammoSlot = FindObjectOfType<Ammo>();
        deathHandler = FindObjectOfType<DeathHandler>();

        if(deathHandler == null)
        {
            Debug.Log("ERROR: DeathHandler script not found.");
        }

        remainingCooldown = 0f;
    }

    void Update()
    {
        if(deathHandler.IsActive) { return; }

        processCooldown();

        if(Input.GetMouseButtonDown(0) && ammoSlot.GetCurrentAmmo(ammoType) > 0 && remainingCooldown <= Mathf.Epsilon)
        {
            Shoot();
        }
    }

    void processCooldown()
    {
        if(remainingCooldown <= Mathf.Epsilon) { return; }

        remainingCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceAmmoAmount(ammoType);

        remainingCooldown = cooldownBetweenShots;
    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.root.GetComponent<EnemyHealth>();  // Enemy Mesh Collider is parented to an empty game object that contains the script.

            if (!target) { return; }

            target.ProcessDamage(weaponDamage);
        }
        else
        {
            return;
        }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        Vector3 impactLocation = hit.point;
        GameObject impactFX = Instantiate(hitImpactEffect, impactLocation, Quaternion.LookRotation(hit.normal));

        Destroy(impactFX, 0.1f);
    }
}
