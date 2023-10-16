namespace Player
{
    /// <summary>
    /// Handles the player health
    /// </summary>
    public class PlayerHealthHandler
    {
        private float m_maxHealth;

        public float PlayerHealth { get; private set; }

        public PlayerHealthHandler(float maxHealth)
        {
            m_maxHealth = maxHealth;
            PlayerHealth = m_maxHealth;
        }

        public void AddToCurrentHealth(float replenishedHealth)
        {
            if (PlayerHealth + replenishedHealth > m_maxHealth)
                PlayerHealth = m_maxHealth;
            else
                PlayerHealth += replenishedHealth;
        }

        public void DecreaseHealth(float damage) => PlayerHealth -= damage;
    }
}