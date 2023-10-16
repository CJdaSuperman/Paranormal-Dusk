using Enums;
using Managers;
using Player;
using System;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Controls the player's guns
    /// </summary>
    public class GunsController : MonoBehaviour
    {
        [SerializeField]
        private Gun[] m_guns;

        [SerializeField]
        private PlayerController m_player;

        private AmmoInventory m_ammoInventory;
        private int m_currentGun = 0;

        public PlayerController Player { get => m_player; }

        public event Action OnGunFired;
        public event Action OnGunReload;
        public event Action OnShowAmmo;
        public event Action OnGunSwitched;

        private void Awake()
        {
            if (!m_player)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(PlayerController)}.");

            m_ammoInventory = new AmmoInventory(m_guns);
        }

        private void Start() => SetCurrentGunActive();

        private void Update()
        {
            if (InputManager.ShowAmmo())
                OnShowAmmo?.Invoke();
        }

        public void ScrollNextGun(bool forward)
        {
            int gunCount = m_guns.Length;
            m_currentGun = (m_currentGun + (forward ? 1 : gunCount - 1)) % gunCount;
            SetCurrentGunActive();
        }

        public void SetNextGun(int gunIndex)
        {
            if (gunIndex >= 0 && gunIndex < m_guns.Length)
            {
                m_currentGun = gunIndex;
                SetCurrentGunActive();
            }
            else
            {
                Debug.LogError($"Attempt to set gun {gunIndex} is invalid");
                return;
            }
        }

        public void GunFired() => OnGunFired?.Invoke();

        public void GunReload() => OnGunReload?.Invoke();

        public int CurrentGunAmmo() => m_guns[m_currentGun].CurrentAmmo;

        public int CurrentGunMagSize() => m_guns[m_currentGun].MagSize;

        public void IncreaseAmmoAmount(AmmoType type, int addition) => m_ammoInventory.IncreaseAmmoAmount(type, addition);

        public void DecreaseAmmoAmount(AmmoType type, int subtraction) => m_ammoInventory.DecreaseAmmoAmount(type, subtraction);

        public int GetAmmoAmount(AmmoType type) => m_ammoInventory.GetAmmoAmount(type);

        private void SetCurrentGunActive()
        {
            int gunsCount = m_guns.Length;

            for (int gun = 0; gun < gunsCount; gun++)
                m_guns[gun].gameObject.SetActive(gun == m_currentGun);

            OnGunSwitched?.Invoke();
        }
    }
}
