using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Handles an enemy's attack behavior
    /// </summary>
    public class EnemyAttackBehavior : MonoBehaviour
    {
        [SerializeField]
        private EnemyController m_enemy;

        [SerializeField]
        private float m_damage = 40f;

        [SerializeField]
        private float m_dealDamageDelay;

        private bool m_attacking;

        private float m_attackTimer;

        private void Awake()
        {
            if (!m_enemy)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(EnemyController)}.");
            else
                m_enemy.OnAttack += Attack;

            m_attacking = false;
        }

        private void Update()
        {
            if (m_attacking)
            {
                m_attackTimer -= Time.deltaTime;

                if (m_attackTimer <= 0f)
                    m_attacking = false;
            }
        }

        private void OnDisable() => m_enemy.OnAttack -= Attack;

        private void Attack()
        {
            if (!m_attacking && m_enemy.Target)
            {
                m_attacking = true;
                m_attackTimer = m_dealDamageDelay;
                m_enemy.Target.Hit(m_damage);
            }
        }
    }
}