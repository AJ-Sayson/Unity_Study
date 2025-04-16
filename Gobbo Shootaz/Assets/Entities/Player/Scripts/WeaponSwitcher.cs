using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int chosenWeaponIndex = 0;
    DeathHandler deathHandler;

    void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = chosenWeaponIndex;

        if(deathHandler.IsActive) { return; }

        ProcessKeyInput();
        ProcessScrollWheel();

        if(previousWeapon != chosenWeaponIndex)
        {
            SetWeaponActive();
        }
    }

    void ProcessKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            chosenWeaponIndex = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            chosenWeaponIndex = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            chosenWeaponIndex = 2;
        }
    }

    void ProcessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(chosenWeaponIndex >= transform.childCount - 1)
            {
                chosenWeaponIndex = 0;
            }
            else
            {
                chosenWeaponIndex++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(chosenWeaponIndex <= 0)
            {
                chosenWeaponIndex = transform.childCount - 1;
            }
            else
            {
                chosenWeaponIndex--;
            }
        }
    }

    void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if(weaponIndex == chosenWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            weaponIndex++;
        }
    }
}
