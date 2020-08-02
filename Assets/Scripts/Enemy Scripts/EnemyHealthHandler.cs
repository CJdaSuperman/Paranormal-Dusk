using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    [SerializeField] float corpseDuration = 4f;

    bool isDead = false;

    public bool IsDead() { return isDead; }

    public void DecreaseEnemyHealth(float damage)
    {
        GetComponent<EnemyAI>().OnDamageTaken();

        healthPoints -= damage;

        if(healthPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(isDead) { return; }  //prevents the animation from happening again when the player shoots the corpse
        
        isDead = true;

        GetComponent<Animator>().SetTrigger("DeathTrigger");

        Transform player = GetComponent<EnemyAI>().GetTarget();
        float playerMaxHealth = player.GetComponent<PlayerHealthHandler>().GetMaxHealth();
        float currentPlayerHealth = player.GetComponent<PlayerHealthHandler>().GetCurrentHealth();

        if (currentPlayerHealth < playerMaxHealth)
        {
            StartCoroutine(FadeCorpse());
        }
    }

    IEnumerator FadeCorpse()
    {
        yield return new WaitForSeconds(corpseDuration);

        GetComponent<CapsuleCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
