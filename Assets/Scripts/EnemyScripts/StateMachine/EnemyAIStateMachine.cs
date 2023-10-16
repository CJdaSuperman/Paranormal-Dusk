namespace Enemy
{
    /// <summary>
    /// The state machine for enemies
    /// </summary>
    public class EnemyAIStateMachine
    {
        private State m_currentState;

        private EnemyController m_enemyController;

        public EnemyAIStateMachine(EnemyController enemyController)
        {
            m_enemyController = enemyController;
            m_currentState = new IdleState();
        }

        public void RunStateMachine()
        {
            State nextState = m_currentState?.RunCurrentState(m_enemyController);

            if (nextState != null)
                m_currentState = nextState;
        }
    }
}
