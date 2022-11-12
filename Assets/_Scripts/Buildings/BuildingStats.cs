using UnityEngine;

namespace _Scripts.Buildings
{
    public class BuildingStats : Stats
    {
        protected BuildingStats()
        {
        }

        public BuildingStats(int health, int maxHealth)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
        }
    }
}
