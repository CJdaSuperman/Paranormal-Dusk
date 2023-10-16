using Player;
using System.Collections;
using UnityEngine;

namespace UI.Menus
{
    /// <summary>
    /// The behavior for the damage indicator canvas
    /// </summary>
    public class DamageIndicatorCanvas : MonoBehaviour
    {
        [SerializeField]
        private Canvas m_canvas;

        [SerializeField]
        private float m_duration;

        [SerializeField]
        private PlayerController m_player;

        private WaitForSeconds m_displayDuration;

        private void Awake()
        {
            if (!m_canvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its canvas.");
            else
                m_canvas.enabled = false;

            if (!m_player)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the player.");
            else
                m_player.OnDamaged += IndicateDamage;

            m_displayDuration = new WaitForSeconds(m_duration);
        }

        private void OnDisable() => m_player.OnDamaged -= IndicateDamage;

        private void IndicateDamage() => StartCoroutine(DisplayIndicator());

        private IEnumerator DisplayIndicator()
        {
            m_canvas.enabled = true;

            yield return m_displayDuration;

            m_canvas.enabled = false;
        }
    }
}