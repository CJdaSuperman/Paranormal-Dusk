using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Handles the visual effects for guns
    /// </summary>
    public class GunVFX : MonoBehaviour
    {
        [SerializeField]
        private Gun m_gun;

        [SerializeField]
        private ParticleSystem m_muzzleFlash;

        [SerializeField]
        private GameObject m_hitFxPrefab;

        [SerializeField]
        private float m_hitFxDuration = 2f;

        private void Awake()
        {
            if (!m_gun)
            {
                Debug.LogError($"{gameObject.name} doesn't have a reference to the gun.");
            }
            else
            {
                m_gun.OnFired += Fired;
                m_gun.OnHit   += Hit;
            }

            if (!m_muzzleFlash)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its muzzle flash FX.");

            if (!m_hitFxPrefab)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the hit FX prefab.");
        }

        private void OnDestroy()
        {
            m_gun.OnFired -= Fired;
            m_gun.OnHit   -= Hit;
        }

        private void Fired() => m_muzzleFlash.Play();

        private void Hit(RaycastHit hit)
        {
            GameObject impact = Instantiate(m_hitFxPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, m_hitFxDuration);
        }
    }
}
