using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] float damage = 40f;

    PlayerHealthHandler target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealthHandler>();
    }

    void AttackAnimationEvent()
    {
        if(target == null) { return; }

        target.DecreaseHealth(damage);
    }
}
