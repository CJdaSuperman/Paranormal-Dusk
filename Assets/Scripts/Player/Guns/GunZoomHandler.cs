using Managers;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Guns
{
    /// <summary>
    /// Handles the zooming for guns
    /// </summary>
    public class GunZoomHandler : MonoBehaviour
    {
        [SerializeField] 
        private Camera m_fpsCamera;

        [SerializeField]
        private RigidbodyFirstPersonController m_fpsController;

        [SerializeField]
        private Gun m_gun;

        [Tooltip("Default Field of View")]
        [SerializeField]
        private float m_normalFOV;

        [Tooltip("Field of View in Aim Down Sight")] 
        [SerializeField] 
        private float m_adsFOV;

        [SerializeField]
        private float m_normalMouseSensitivity;

        [SerializeField] 
        private float m_adsMouseSensitivity;

        private bool m_isADS = false;

        private void Awake()
        {
            if (!m_fpsCamera)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the camera.");

            if (!m_fpsController)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the {nameof(RigidbodyFirstPersonController)}.");

            if (!m_gun)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the gun.");
            else
                m_gun.OnReload += NormalView;
        }

        private void Start() => m_gun.Animator.SetADS(false);

        private void Update()
        {
            if (GameManager.IsGamePaused)
                return;

            if (InputManager.AimDown())
            {
                if (!m_isADS)
                    AimDown();
            }

            if (InputManager.ReleaseAimDown())
                NormalView();
        }

        private void OnDisable() => NormalView();

        private void OnDestroy() => m_gun.OnReload -= NormalView;

        private void AimDown()
        {
            m_isADS = true;

            m_gun.Animator.SetADS(m_isADS);

            m_fpsCamera.fieldOfView = m_adsFOV;

            m_fpsController.mouseLook.XSensitivity = m_adsMouseSensitivity;
            m_fpsController.mouseLook.YSensitivity = m_adsMouseSensitivity;
        }

        private void NormalView()
        {
            m_isADS = false;

            m_gun.Animator.SetADS(m_isADS);

            m_fpsCamera.fieldOfView = m_normalFOV;

            m_fpsController.mouseLook.XSensitivity = m_normalMouseSensitivity;
            m_fpsController.mouseLook.YSensitivity = m_normalMouseSensitivity;
        }
    }
}