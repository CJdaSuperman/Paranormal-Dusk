using Managers;
using Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    /// <summary>
    /// Controls enemy GameObjects
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private CapsuleCollider m_bodyCollider;

        [Tooltip("The collider to detect player is within chase range")]
        [SerializeField]
        private SphereCollider m_sphereCollider;

        [SerializeField]
        private NavMeshAgent m_agent;

        [SerializeField]
        private DeathParticles m_deathParticlesPrefab;

        [Header("Attributes")]
        [SerializeField]
        private float m_healthPoints = 100f;

        [SerializeField]
        private float m_chaseRange = 5f;

        [SerializeField]
        private float m_turnSpeed = 5f;

        [SerializeField]
        private float m_corpseDuration = 4f;

        EnemyAIStateMachine m_ai;

        private float m_health;

        private WaitForSeconds m_fadeCorpseDelay;

        private Vector3 m_targetDirection = Vector3.zero;
        private Vector3 m_targetDistance = Vector3.zero;

        public PlayerController Target { get; private set; }

        public event Action OnIdle;
        public event Action OnChase;
        public event Action OnAttack;
        public event Action OnDeath;

        /// <summary>
        /// Visual representation of the chase range
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_chaseRange);
        }

        private void Awake()
        {
            if (!m_bodyCollider)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its body collider.");

            if (!m_sphereCollider)
            {
                Debug.LogError($"{gameObject.name} doesn't have a reference to its sphere collider.");
            }
            else
            {
                m_sphereCollider.radius = m_chaseRange;
                m_sphereCollider.isTrigger = true;
            }

            if (!m_agent)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the NavMeshAgent.");

            m_health = m_healthPoints;
            m_ai = new EnemyAIStateMachine(this);
            m_fadeCorpseDelay = new WaitForSeconds(m_corpseDuration);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!Target && other.TryGetComponent(out PlayerController player))
                Target = player;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerController>())
                Target = null;
        }

        private void LateUpdate()
        {
            if (GameManager.IsGamePaused)
                return;

            m_ai.RunStateMachine();
        }
            

        public void Hit(float damage, PlayerController player)
        {
            if (m_health > 0)
            {
                m_health -= damage;

                if (m_health <= 0)
                    Die(player);
                else
                    Target = player;
            }
        }

        public void Chase()
        {
            FaceTarget();
            m_agent.SetDestination(Target.transform.position);
            OnChase?.Invoke();
        }

        public void Idle() => OnIdle?.Invoke();

        public void Attack() => OnAttack?.Invoke();

        public bool WithinAttackDistance()
        {
            Vector3 position = transform.position;
            Vector3 targetPosition = Target.transform.position;

            m_targetDistance.Set(targetPosition.x - position.x,
                                 targetPosition.y - position.y,
                                 targetPosition.z - position.z);

            return m_targetDistance.magnitude <= m_agent.stoppingDistance;
        }

        private void FaceTarget()
        {
            Vector3 position       = transform.position;
            Vector3 targetPosition = Target.transform.position;

            m_targetDirection.Set(targetPosition.x - position.x,
                                  targetPosition.y - position.y,
                                  targetPosition.z - position.z);
            m_targetDirection = m_targetDirection.normalized;
            m_targetDirection.y = 0f;

            Quaternion lookRotation = Quaternion.LookRotation(m_targetDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);
        }

        private void Die(PlayerController player)
        {
            enabled = false;
            m_agent.enabled = false;
            OnDeath?.Invoke();
            StartCoroutine(FadeCorpse(player.CurrentHealth < player.MaxHealth));
        }

        private IEnumerator FadeCorpse(bool enableParticles)
        {
            m_bodyCollider.enabled = false;

            if (enableParticles)
                Instantiate(m_deathParticlesPrefab, transform.position, m_deathParticlesPrefab.transform.rotation);

            yield return m_fadeCorpseDelay;

            Destroy(gameObject);
        }
    }
}
