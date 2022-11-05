namespace _Scripts
{
    public class CharacterStats
    {
        protected internal int CurrentHealth { get; protected set; }
        protected int MaxHealth { get; set; }
        protected int Damage { get; set; }
        protected int Armor { get; set; }

        protected CharacterStats()
        {
            // initialize to 0
            CurrentHealth = 0;
            MaxHealth = 0;
            Damage = 0;
            Armor = 0;
        }

        public CharacterStats(int health, int maxHealth, int damage, int armor)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
            Damage = damage;
            Armor = armor;
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