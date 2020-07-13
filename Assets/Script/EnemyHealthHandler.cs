using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;

    public void DecreaseEnemyHealth(float damage)
    {
        GetComponent<EnemyAI>().OnDamageTaken();

        healthPoints -= damage;

        if(healthPoints == 0)
        {
            Destroy(gameObject);
        }
    }
}
