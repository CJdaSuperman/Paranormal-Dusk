using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    /// <summary>
    /// The button behavior for quitting game
    /// </summary>
    public class QuitButton : MonoBehaviour
    {
        [SerializeField]
        private Button m_button;

        private void Awake()
        {
            if (!m_button)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its Button component.");
            else
                m_button.onClick.AddListener(Quit);
        }

        private void Quit() => Application.Quit();
    }
}
