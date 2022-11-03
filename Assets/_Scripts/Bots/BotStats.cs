namespace _Scripts.Bots
{
    public class BotStats : CharacterStats
    {
        protected BotStats()
        {
        }

        public BotStats(int health, int maxHealth)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
        }
    }
}