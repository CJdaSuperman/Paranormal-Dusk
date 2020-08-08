using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas crosshairCanvas;
    [SerializeField] Canvas playerHealthCanvas;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Canvas gameOverCanvas;

    bool gamePaused = false;

    PlayerHealthHandler player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerHealthHandler>();
        gameOverCanvas.enabled = false;
        Time.timeScale = 1f;    //fixes bug where timeScale is 0 after reloading scene
    }

    void Update()
    {
        if(player.GetCurrentHealth() <= 0)
            EndGame();
        
        if(Input.GetKeyDown(KeyCode.Tab))
            TogglePauseMenu();
    }

    void EndGame()
    {
        Time.timeScale = 0;

        gameOverCanvas.enabled = true;
        crosshairCanvas.enabled = false;
        playerHealthCanvas.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gamePaused = true;
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        PauseGame();
    }

    void PauseGame()
    {
        if (pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            gamePaused = false;
        }
    }

    public bool isGamePaused() { return gamePaused; }
}