namespace _Scripts
{
    public class CharacterStats
    {
        protected internal int CurrentHealth { get; protected set; }
        protected int MaxHealth { get; set; }

        protected CharacterStats()
        {
            // initialize to 0
            CurrentHealth = 0;
            MaxHealth = 0;
        }

        public CharacterStats(int health, int maxHealth)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
        }

        public void TakeDamage(int dmgAmount)
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= dmgAmount;
            }
        }
    }
}