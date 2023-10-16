using Enums;
using Guns;
using UnityEngine;

namespace Pickups
{
    /// <summary>
    /// The behavior for ammo pickups
    /// </summary>
    public class AmmoPickup : MonoBehaviour
    {
        [SerializeField]
        private AmmoType m_ammoType;

        [SerializeField]
        private int m_ammoGiveAmount;

        public AmmoType Type { get => m_ammoType; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GunsController player))
            {
                player.IncreaseAmmoAmount(m_ammoType, m_ammoGiveAmount);
                Destroy(gameObject);
            }
        }
    }
}