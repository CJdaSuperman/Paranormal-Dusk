using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas crosshairCanvas;
    [SerializeField] Canvas playerHealthCanvas;

    void Start()
    {
        gameOverCanvas.enabled = false;    
    }

    public void Die()
    {
        Time.timeScale = 0;

        GetComponentInChildren<GunSwitcher>().enabled = false;

        gameOverCanvas.enabled = true;
        crosshairCanvas.enabled = false;
        playerHealthCanvas.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
