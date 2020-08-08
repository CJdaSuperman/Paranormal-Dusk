using UnityEngine;

public class AmmoPickupHandler : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoGiveAmount = 12;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AmmoHandler>().IncreaseCurrentAmmo(ammoType, ammoGiveAmount);

            other.gameObject.GetComponentInChildren<AmmoHandler>().ShowAmmoInventory();

            Destroy(gameObject);
        }
    }
}