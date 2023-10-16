namespace Enemy
{
    /// <summary>
    /// The chase state within an enemy's state machine
    /// </summary>
    public class ChaseState : State
    {
        public ChaseState()
        {
            // There's two different states Chase can go into: Idle or Attack
            this.m_nextState = null;
        }

        public override State RunCurrentState(EnemyController enemyController)
        {
            if (enemyController.Target)
            {
                if (enemyController.WithinAttackDistance())
                {
                    return new AttackState();
                }
                else
                {
                    enemyController.Chase();
                    return this;
                }
            }

            return new IdleState();
        }
    }
}
