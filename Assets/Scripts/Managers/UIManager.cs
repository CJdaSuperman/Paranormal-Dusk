using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages UI canvases
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Canvas m_crosshairCanvas;

        [SerializeField]
        private Canvas m_playerHealthCanvas;
        
        [SerializeField]
        private Canvas m_pauseMenu;
        
        [SerializeField]
        private Canvas m_gameOverMenu;

        private GameObject PauseMenu          { get => m_pauseMenu.gameObject; }
        private GameObject GameOverMenu       { get => m_gameOverMenu.gameObject; }

        private void Awake()
        {
            if (!m_crosshairCanvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the crosshair canvas.");

            if (!m_playerHealthCanvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the crosshair canvas.");
            
            if (!m_pauseMenu)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the crosshair canvas.");

            if (!m_gameOverMenu)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the crosshair canvas.");

            GameManager.OnGameLost += ShowGameOver;
        }

        private void Start()
        {
            PauseMenu.SetActive(false);
            GameOverMenu.SetActive(false);
        }

        private void Update()
        {
            if (InputManager.PauseGame())
                TogglePause();
        }

        private void OnDisable() => GameManager.OnGameLost -= ShowGameOver;

        public void TogglePause()
        {
            bool pauseMenuActive = PauseMenu.activeInHierarchy;
            PauseMenu.SetActive(!pauseMenuActive);
            GameManager.PauseGame(!pauseMenuActive);
        }

        private void ShowGameOver()
        {
            m_crosshairCanvas.enabled = false;
            m_playerHealthCanvas.enabled = false;
            GameOverMenu.SetActive(true);
        }
    }
}