using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    [SerializeField] Canvas playerHealthCanvas;
    [SerializeField] Canvas damageIndicatorCanvas;
    [SerializeField] TextMeshProUGUI playerHealthText;

    public void DecreaseHealth(float damage)
    {
        playerHealth -= damage;

        damageIndicatorCanvas.GetComponent<DamageIndicatorController>().IndicateDamage();

        if(playerHealth <= 0)
        {
            GetComponent<DeathHandler>().Die();
        }
    }

    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.CapsLock))
        {
            playerHealthText.text = playerHealth.ToString();
            playerHealthCanvas.GetComponent<PlayerHealthCanvasHandler>().FadeCanvas();
        }
    }
}
