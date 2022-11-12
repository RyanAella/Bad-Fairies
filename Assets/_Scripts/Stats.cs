﻿using UnityEngine;

namespace _Scripts
{
    public class Stats
    {
        protected internal int CurrentHealth { get; protected set; }
        protected int MaxHealth { get; set; }
        protected int Damage { get; set; }
        protected int Armor { get; set; }

        protected Stats()
        {
            // initialize to 0
            CurrentHealth = 0;
            MaxHealth = 0;
            Damage = 0;
            Armor = 0;
        }

        public Stats(int health, int maxHealth)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
        }

        public Stats(int health, int maxHealth, int damage, int armor)
        {
            CurrentHealth = health;
            MaxHealth = maxHealth;
            Damage = damage;
            Armor = armor;
        }

        public void TakeDamage(int dmgAmount)
        {
            Debug.Log("Take " + dmgAmount + " damage.");
            if (CurrentHealth > 0)
            {
                CurrentHealth -= dmgAmount;
            }
        }
    }
}