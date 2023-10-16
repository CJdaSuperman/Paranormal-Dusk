using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Buttons
{
    /// <summary>
    /// The button behavior for reloading the scene
    /// </summary>
    public class ReloadButton : MonoBehaviour
    {
        [SerializeField]
        private Button m_button;

        private void Awake()
        {
            if (!m_button)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its Button component.");
            else
                m_button.onClick.AddListener(Reload);
        }

        private void Reload()
        {
            GameManager.EnableTimeScale(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
