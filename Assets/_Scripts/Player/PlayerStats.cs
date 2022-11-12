namespace _Scripts.Player
{
    public class PlayerStats : Stats
    {
        protected PlayerStats()
        {
        }

        public PlayerStats(int health, int maxHealth, int damage, int armor)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
            Damage = damage;
            Armor = armor;
        }
    }
}