using UnityEngine;

namespace _Scripts
{
    public class Stats : MonoBehaviour
    {
        public int CurrentHealth { get; private set; }
        
        public int MaxHealth;
        public int Damage;
        public int Armor;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }
        public void TakeDamage(int incommingDamage)
        {            
            if (CurrentHealth > 0)
            {
                Debug.Log("Incomming damage is: " + incommingDamage);
                // reduce damage by armor
                var damage = incommingDamage - Armor;
                Debug.Log("Total damage taken is: " + damage);

                // check if damage is negativ or 0 if so set to one
                if (damage <= 0) damage = 1;

                /* DEBUG ONLY set damage to 0 */
                damage = 0;

                CurrentHealth -= damage;
            }
        }
    }
}