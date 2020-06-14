using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] float ChaseRange = 5f;

    NavMeshAgent Agent;
    float DistanceToTarget = Mathf.Infinity;    //avoids chasing after the player right away
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        DistanceToTarget = Vector3.Distance(Target.position, transform.position);

        if(DistanceToTarget <= ChaseRange)
            Agent.SetDestination(Target.position);

    }
}
