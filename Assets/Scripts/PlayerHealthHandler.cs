using TMPro;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    [SerializeField] Canvas playerHealthCanvas;
    [SerializeField] Canvas damageIndicatorCanvas;
    [SerializeField] TextMeshProUGUI playerHealthText;

    float maxHealth;

    GameManager gameManager;

    void Start()
    {
        maxHealth = playerHealth;

        gameManager = FindObjectOfType<GameManager>();
    }

    public void DecreaseHealth(float damage)
    {
        playerHealth -= damage;

        damageIndicatorCanvas.GetComponent<DamageIndicatorController>().IndicateDamage();
    }

    void Update()
    {        
        if(gameManager.isGamePaused()) { return; }
        
        if(Input.GetKeyDown(KeyCode.CapsLock))
            ShowPlayerHealthCanvas();
    }

    public void ShowPlayerHealthCanvas()
    {
        playerHealthText.text = playerHealth.ToString();
        playerHealthCanvas.GetComponent<PlayerHealthCanvasHandler>().FadeCanvas();
    }
    
    public float GetMaxHealth() { return maxHealth; }

    public float GetCurrentHealth() { return playerHealth; }  
    
    public void AddToCurrentHealth(float replenishedHealth) 
    {
        if (playerHealth + replenishedHealth > maxHealth)
            playerHealth = maxHealth;
        else
            playerHealth += replenishedHealth;
    }
}