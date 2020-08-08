using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameManager gameManager;

    void Start() => gameManager = FindObjectOfType<GameManager>();

    public void Resume()
    {
        gameManager.TogglePauseMenu();
    }

    public void Quit() => Application.Quit();
}
