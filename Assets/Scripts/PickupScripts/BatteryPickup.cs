using Player;
using UnityEngine;

namespace Pickups
{
    /// <summary>
    /// The behavior for battery pickups
    /// </summary>
    public class BatteryPickup : MonoBehaviour
    {
        [SerializeField]
        private float m_lightAngle;

        [SerializeField]
        private float m_lightIntensity;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                player.PickupBattery(m_lightAngle, m_lightIntensity);
                Destroy(gameObject);
            }
        }
    }
}