using Player;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// The behavior for death particles
    /// </summary>
    [RequireComponent(typeof(SphereCollider))]
    public class DeathParticles : MonoBehaviour
    {
        [SerializeField]
        private SphereCollider m_sphereCollider;

        [SerializeField]
        private float m_replenishAmount;

        private void Awake()
        {
            if (!m_sphereCollider)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its SphereCollider.");
            else
                m_sphereCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                player.AddHealth(m_replenishAmount);
                Destroy(gameObject);
            }
        }
    }
}