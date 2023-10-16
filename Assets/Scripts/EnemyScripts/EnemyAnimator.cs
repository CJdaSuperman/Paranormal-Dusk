using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Controls the animator of enemies
    /// </summary>
    public class EnemyAnimator : MonoBehaviour
    {
        private const string AttackConditionName = "AttackCondition";
        private const string IdleTriggerName     = "IdleTrigger";
        private const string MoveTriggerName     = "MoveTrigger";
        private const string DeathTriggerName    = "DeathTrigger";

        private static readonly int AttackCondition = Animator.StringToHash(AttackConditionName);
        private static readonly int IdleTrigger     = Animator.StringToHash(IdleTriggerName);
        private static readonly int MoveTrigger     = Animator.StringToHash(MoveTriggerName);
        private static readonly int DeathTrigger    = Animator.StringToHash(DeathTriggerName);

        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private EnemyController m_enemy;

        private void Awake()
        {
            if (!m_animator)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its animator.");

            if (!m_enemy)
            {
                Debug.LogError($"{gameObject.name} doesn't have a reference to the {nameof(EnemyController)}.");
            }
            else
            {
                m_enemy.OnIdle   += Idle;
                m_enemy.OnChase  += Chase;
                m_enemy.OnAttack += Attack;
                m_enemy.OnDeath  += Die;
            } 
        }

        private void OnDisable()
        {
            m_enemy.OnIdle   -= Idle;
            m_enemy.OnChase  -= Chase;
            m_enemy.OnAttack -= Attack;
            m_enemy.OnDeath  -= Die;
        }

        private void Idle() => m_animator.SetTrigger(IdleTrigger);

        private void Chase()
        {
            m_animator.SetBool(AttackCondition, false);
            m_animator.SetTrigger(MoveTrigger);
        }

        private void Attack() => m_animator.SetBool(AttackCondition, true);

        private void Die() => m_animator.SetTrigger(DeathTrigger);
    }
}
