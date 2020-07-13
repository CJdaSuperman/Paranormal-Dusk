using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;

    public void DecreaseHealth(float damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0)
        {
            GetComponent<DeathHandler>().Die();
        }
    }
}
