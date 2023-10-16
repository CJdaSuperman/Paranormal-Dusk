using Enums;
using Player;
using Managers;
using System;
using UnityEngine;

namespace Pickups
{
    /// <summary>
    /// The behavior of ammo boxes
    /// </summary>
    public class AmmoBox : MonoBehaviour
    {
        [SerializeField]
        private AmmoPickup m_ammoPickup;

        private bool m_isPlayerNear = false;

        private bool m_isBoxOpen = false;

        public static event Action<AmmoType> OnPlayerEntered;
        public static event Action OnPlayerExit;

        private void Awake()
        {
            if (!m_ammoPickup)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its {nameof(AmmoPickup)} GameObject.");
        }

        private void Update()
        {
            if (m_isPlayerNear && !m_isBoxOpen && 
                InputManager.Interact()        &&
                !GameManager.IsGamePaused)
            {
                m_ammoPickup.gameObject.SetActive(true);
                m_isBoxOpen = true;
                OnPlayerExit?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!m_isBoxOpen && other.TryGetComponent(out PlayerController player))
            {
                m_isPlayerNear = true;
                OnPlayerEntered?.Invoke(m_ammoPickup.Type);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!m_isBoxOpen && other.TryGetComponent(out PlayerController player))
            {
                m_isPlayerNear = false;
                OnPlayerExit?.Invoke();
            }
        }
    }
}