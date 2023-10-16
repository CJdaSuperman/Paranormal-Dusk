using Guns;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// The text to display the amount of ammo left in a gun
    /// </summary>
    public class CurrentAmmoAmountText : UiText
    {
        [SerializeField]
        private GunsController m_gunsController;

        private void Awake()
        {
            base.Awake();

            if (!m_gunsController)
            {
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(GunsController)}.");
            }
            else
            {
                m_gunsController.OnGunFired    += UpdateText;
                m_gunsController.OnGunReload   += UpdateText;
                m_gunsController.OnGunSwitched += UpdateText;
            }
        }

        private void OnDisable()
        {
            m_gunsController.OnGunFired    -= UpdateText;
            m_gunsController.OnGunReload   -= UpdateText;
            m_gunsController.OnGunSwitched -= UpdateText;
        }

        protected override void UpdateText() => m_text.text = m_gunsController.CurrentGunAmmo().ToString();
    }
}