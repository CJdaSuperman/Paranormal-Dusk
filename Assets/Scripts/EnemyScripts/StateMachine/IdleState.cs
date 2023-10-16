namespace Enemy
{
    /// <summary>
    /// The idle state within the enemy's state machine
    /// </summary>
    public class IdleState : State
    {
        public IdleState()
        {
            this.m_nextState = new ChaseState();
        }

        public override State RunCurrentState(EnemyController enemyController)
        {
            if (enemyController.Target)
                return m_nextState;

            enemyController.Idle();
            return this;
        }
    }
}
