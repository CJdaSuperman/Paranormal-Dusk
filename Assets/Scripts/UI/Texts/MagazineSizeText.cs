using Guns;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// The text to display the size of a gun's magazine
    /// </summary>
    public class MagazineSizeText : UiText
    {
        [SerializeField]
        private GunsController m_gunController;

        private void Awake()
        {
            base.Awake();

            if (!m_gunController)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(GunsController)}.");
            else
                m_gunController.OnGunSwitched += UpdateText;
        }

        private void OnDisable() => m_gunController.OnGunSwitched -= UpdateText;

        protected override void UpdateText() => m_text.text = $"/{m_gunController.CurrentGunMagSize()}";
    }
}