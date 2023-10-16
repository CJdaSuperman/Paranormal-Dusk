using Player;
using UI.Menus;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// The text to display player health
    /// </summary>
    public class PlayerHealthText : UiText
    {
        [SerializeField]
        private PlayerHealthCanvas m_playerHealthCanvas;

        [SerializeField]
        private PlayerController m_player;

        private void Awake()
        {
            base.Awake();

            if (!m_playerHealthCanvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(PlayerHealthCanvas)}.");
            else
                m_playerHealthCanvas.OnShowed += UpdateText;

            if (!m_player)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the player health.");
        }

        private void OnDestroy() => m_playerHealthCanvas.OnShowed -= UpdateText;

        protected override void UpdateText() => m_text.text = m_player.CurrentHealth.ToString();
    }
}
