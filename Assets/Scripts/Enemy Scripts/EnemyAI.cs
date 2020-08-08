using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent agent;
    float distanceToTarget = Mathf.Infinity;    //avoids chasing after the player right away
    bool isProvoked = false;

    Transform target;

    Animator animator;

    void Start()
    {
        target = FindObjectOfType<PlayerHealthHandler>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(GetComponent<EnemyHealthHandler>().IsDead()) 
        { 
            enabled = false;
            agent.enabled = false;
        }
        
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)        
            EngageTarget();        
        else if (distanceToTarget <= chaseRange)        
            isProvoked = true;                    
    }

    void EngageTarget()
    {
        FaceTarget(); 
        
        if(distanceToTarget >= agent.stoppingDistance)        
            ChaseTarget();        

        if(distanceToTarget <= agent.stoppingDistance)        
            AttackTarget();        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void ChaseTarget()
    {
        animator.SetBool("AttackCondition", false);
        animator.SetTrigger("MoveTrigger");

        agent.SetDestination(target.position);
    }

    void AttackTarget() => animator.SetBool("AttackCondition", true);

    public void OnDamageTaken() => isProvoked = true;

    public Transform GetTarget() { return target; }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}