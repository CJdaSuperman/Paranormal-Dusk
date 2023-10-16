namespace Enemy
{
    /// <summary>
    /// The state within an enemy's state machine
    /// </summary>
    public abstract class State
    {
        protected State m_nextState;

        public abstract State RunCurrentState(EnemyController enemyController);
    }
}
