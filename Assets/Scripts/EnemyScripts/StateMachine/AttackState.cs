namespace Enemy
{
    /// <summary>
    /// The attack state within an enemy's state machine
    /// </summary>
    public class AttackState : State
    {
        public AttackState()
        {
            this.m_nextState = new ChaseState();
        }

        public override State RunCurrentState(EnemyController enemyController)
        {
            if (!enemyController.WithinAttackDistance())
                return m_nextState;

            enemyController.Attack();
            return this;
        }
    }
}