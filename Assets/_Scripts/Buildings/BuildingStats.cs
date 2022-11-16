namespace _Scripts.Buildings
{
    public class BuildingStats : Stats
    {
        public BuildingStats(int health, int maxHealth)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
        }
    }
}
