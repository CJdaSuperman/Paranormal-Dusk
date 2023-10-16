using Enums;
using Pickups;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Menus
{
    /// <summary>
    /// The behavior for the ammo pickup canvas
    /// </summary>
    public class AmmoPickupCanvas : MonoBehaviour
    {
        [SerializeField]
        private Canvas m_canvas;

        [SerializeField]
        private Color m_pistolPickupTextColor;

        [SerializeField]
        private Color m_shellPickupTextColor;

        [SerializeField]
        private Color m_sniperPickupTextColor;

        [SerializeField]
        private TextMeshProUGUI[] m_texts;

        private Dictionary<AmmoType, Color> m_pickupColors;

        private void Awake()
        {
            if (!m_canvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its canvas.");
            else
                HideCanvas();

            m_pickupColors = new Dictionary<AmmoType, Color>();
            m_pickupColors.Add(AmmoType.Bullets, m_pistolPickupTextColor);
            m_pickupColors.Add(AmmoType.Shells, m_shellPickupTextColor);
            m_pickupColors.Add(AmmoType.SniperCartridges, m_sniperPickupTextColor);
        }

        private void OnEnable()
        {
            AmmoBox.OnPlayerEntered += DisplayInteraction;
            AmmoBox.OnPlayerExit    += HideCanvas;
        }

        private void OnDisable()
        {
            AmmoBox.OnPlayerEntered -= DisplayInteraction;
            AmmoBox.OnPlayerExit    -= HideCanvas;
        }

        private void DisplayInteraction(AmmoType ammoType)
        {
            m_canvas.enabled = true;

            foreach (TextMeshProUGUI text in m_texts)
                text.color = m_pickupColors[ammoType];
        }

        private void HideCanvas() => m_canvas.enabled = false;
    }
}
