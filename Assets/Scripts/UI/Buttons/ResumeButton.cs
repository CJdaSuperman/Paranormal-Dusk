using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    /// <summary>
    /// The button behavior for resuming the game from a paused state
    /// </summary>
    public class ResumeButton : MonoBehaviour
    {
        [SerializeField]
        private Button m_button;

        [SerializeField]
        private UIManager m_uiManager;

        private void Awake()
        {
            if (!m_button)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its Button component.");
            else
                m_button.onClick.AddListener(Resume);
        }

        private void Resume() => m_uiManager.TogglePause();
    }
}
