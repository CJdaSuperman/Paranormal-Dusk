using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [System.Serializable]
    public class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount = 24;
    }
    
    [SerializeField] AmmoSlot[] ammoInventory;    

    //public AmmoType GetAmmoType() { return ammoType; }
    
    //public int GetAmmoAmount() { return ammoAmount; }

    //TODO - used for gun reloading
    public void ReduceAmmo(AmmoType ammoType, int magSize) => GetAmmoSlot(ammoType).ammoAmount -= magSize;

    public void IncreaseCurrentAmmo(AmmoType ammoType, int additionalAmmo) => GetAmmoSlot(ammoType).ammoAmount += additionalAmmo;

    public AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoInventory)
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }

        return null;
    }
}
