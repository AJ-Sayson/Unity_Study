using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType) {
        int currentAmmo = GetAmmoSlot(ammoType).ammoAmount;

        return currentAmmo;
    }

    public void IncreaseAmmoAmount(AmmoType ammoType, int increaseAmount)
    {
        AmmoSlot currentAmmoSlot = GetAmmoSlot(ammoType);

        currentAmmoSlot.ammoAmount += increaseAmount;
    }

    public void ReduceAmmoAmount(AmmoType ammoType)
    {
        AmmoSlot currentAmmoSlot = GetAmmoSlot(ammoType);

        currentAmmoSlot.ammoAmount -= 1;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }

        return null;
    }
}
