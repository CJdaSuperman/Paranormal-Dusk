using Enemy;
using Enums;
using Managers;
using System;
using System.Collections;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// The behavior of a gun
    /// </summary>
    public class Gun : MonoBehaviour
    {
        [SerializeField] 
        private Camera m_camera;

        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private GunsController m_gunsController;

        [Header("Attributes")]
        [SerializeField] 
        private AmmoType m_ammoType;

        [SerializeField] 
        private float m_firingRange;

        [SerializeField] 
        private float m_timeBetweenShots;

        [SerializeField] 
        private int m_damageDeals;

        [SerializeField] 
        private int m_magSize;

        [SerializeField] 
        private float m_reloadSpeed;

        private bool m_reloading = false;
        private bool m_shooting  = false;

        private WaitForSeconds m_shootDelay;
        private WaitForSeconds m_reloadDuration;

        private IEnumerator m_currentRoutine;

        public AmmoType TypeAmmo { get => m_ammoType; }
        public int CurrentAmmo { get; private set; }
        public int MagSize { get => m_magSize; }

        public GunAnimator Animator { get; private set; }

        public event Action OnFired;
        public event Action OnReload;
        public event Action<RaycastHit> OnHit;

        private void Awake()
        {
            if (!m_camera)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the camera.");

            if (!m_animator)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its animator.");
            else
                Animator = new GunAnimator(m_animator);

            if (!m_gunsController)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(GunsController)}.");

            m_shootDelay     = new WaitForSeconds(m_timeBetweenShots);
            m_reloadDuration = new WaitForSeconds(m_reloadSpeed);
            CurrentAmmo = m_magSize;
        }

        private void Update()
        {
            if (m_reloading || GameManager.IsGamePaused)
                return;

            if(InputManager.Shoot() && !m_shooting)
                Shoot();

            if (InputManager.Reload()     && 
                CurrentAmmo != m_magSize  &&
                m_gunsController.GetAmmoAmount(m_ammoType) > 0)
            {
                StartRoutine(Reload());
            }
        }

        private void OnDisable()
        {
            m_shooting = false;

            m_reloading = false;
            Animator.SetReloading(m_reloading);

            if (m_currentRoutine != null)
                StopCoroutine(m_currentRoutine);
        }

        private void Shoot()
        {
            if (CurrentAmmo > 0)
                StartRoutine(Fire());
            else if (CurrentAmmo <= 0 && m_gunsController.GetAmmoAmount(m_ammoType) > 0)
                StartRoutine(Reload());
        }

        private IEnumerator Fire()
        {
            ProcessRayCast();
            
            CurrentAmmo--;
            m_shooting = true;

            m_gunsController.GunFired();
            
            OnFired?.Invoke();

            yield return m_shootDelay;

            m_shooting = false;
        }

        private IEnumerator Reload()
        {
            m_reloading = true;

            Animator.SetReloading(m_reloading);

            OnReload?.Invoke();

            yield return m_reloadDuration;

            m_reloading = false;

            Animator.SetReloading(m_reloading);

            UpdateAmmoInventory();

            m_gunsController.GunReload();
        }

        private void ProcessRayCast()
        {
            Transform camera = m_camera.transform.transform;

            if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, m_firingRange))
            {
                if (hit.transform.TryGetComponent(out EnemyController enemy))
                    enemy.Hit(m_damageDeals, m_gunsController.Player);

                OnHit?.Invoke(hit);
            }
        }

        private void UpdateAmmoInventory()
        {
            // If there will be leftover ammo in the inventory after reloading, the current ammo will be the magSize;
            // otherwise, use remaining ammo in inventory to reload.
            if (CurrentAmmo + m_gunsController.GetAmmoAmount(m_ammoType) > m_magSize)
            {
                int ammoUsed = m_magSize - CurrentAmmo;
                m_gunsController.DecreaseAmmoAmount(m_ammoType, ammoUsed);
                CurrentAmmo = m_magSize;
            }
            else
            {
                CurrentAmmo += m_gunsController.GetAmmoAmount(m_ammoType);
                m_gunsController.DecreaseAmmoAmount(m_ammoType, m_gunsController.GetAmmoAmount(m_ammoType));
            }
        }

        private void StartRoutine(IEnumerator routine)
        {
            m_currentRoutine = routine;
            StartCoroutine(m_currentRoutine);
        }
    }
}
