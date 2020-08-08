using TMPro;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [System.Serializable]
    public class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount = 0;
    }
    
    [SerializeField] AmmoSlot[] ammoInventory;

    [SerializeField] Canvas ammoInventoryCanvas;
    [SerializeField] TextMeshProUGUI bulletsCount;
    [SerializeField] TextMeshProUGUI shellsCount;
    [SerializeField] TextMeshProUGUI cartridgesCount;

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


    public void ShowAmmoInventory()
    {       
        bulletsCount.text = (ammoInventory[0].ammoAmount).ToString();
        shellsCount.text = (ammoInventory[1].ammoAmount).ToString();
        cartridgesCount.text = (ammoInventory[2].ammoAmount).ToString();
        
        ammoInventoryCanvas.GetComponent<AmmoInventoryCanvasHandler>().FadeCanvas();
    }
}