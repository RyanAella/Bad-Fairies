namespace _Scripts.Player
{
    public class PlayerStats : CharacterStats
    {
        protected PlayerStats()
        {
        }

        public PlayerStats(int health, int maxHealth)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
        }
    }
}