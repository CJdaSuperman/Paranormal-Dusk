using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;

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
    }
}
