﻿namespace _Scripts.Bots
{
    public class BotStats : Stats
    {
        protected BotStats()
        {
        }

        public BotStats(int health, int maxHealth, int damage, int armor)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
            Damage = damage;
            Armor = armor;
        }
    }
}