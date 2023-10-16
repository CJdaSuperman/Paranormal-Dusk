using Managers;
using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Controls the player GameObject
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float m_maxHealth;

        [SerializeField]
        private Flashlight m_flashlight;

        private PlayerHealthHandler m_healthHandler;

        public float MaxHealth { get => m_maxHealth; }
        public float CurrentHealth { get => m_healthHandler.PlayerHealth; }

        public event Action OnShowHealth;
        public event Action OnDamaged;

        private void Awake()
        {
            if (!m_flashlight)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the flashlight.");

            m_healthHandler = new PlayerHealthHandler(m_maxHealth);
        }

        private void Update()
        {
            if (GameManager.IsGamePaused)
                return;

            if (InputManager.ShowHealth())
                OnShowHealth?.Invoke();
        }

        public void Hit(float damageTaken)
        {
            m_healthHandler.DecreaseHealth(damageTaken);

            if (m_healthHandler.PlayerHealth <= 0)
                GameManager.EndGame();
            else
                OnDamaged?.Invoke();
        }

        public void AddHealth(float replenishAmount)
        {
            m_healthHandler.AddToCurrentHealth(replenishAmount);
            OnShowHealth?.Invoke();
        }

        public void PickupBattery(float lightAngle, float lightIntensity) => m_flashlight.Restore(lightAngle, lightIntensity);
    }
}