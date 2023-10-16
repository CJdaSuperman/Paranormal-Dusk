using Enums;
using Guns;
using UI.Menus;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// The text to display ammount amount
    /// </summary>
    public class AmmoAmountText : UiText
    {
        [SerializeField]
        private AmmoType m_ammoType;

        [SerializeField]
        private AmmoInventoryCanvas m_ammoInventoryCanvas;

        [SerializeField]
        private GunsController m_gunController;

        private void Awake()
        {
            base.Awake();

            if (!m_ammoInventoryCanvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(AmmoInventoryCanvas)}.");
            else
                m_ammoInventoryCanvas.OnShowed += UpdateText;

            if (!m_gunController)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(GunsController)}.");
        }

        private void OnDestroy() => m_ammoInventoryCanvas.OnShowed -= UpdateText;

        protected override void UpdateText() => m_text.text = m_gunController.GetAmmoAmount(m_ammoType).ToString();
    }
}