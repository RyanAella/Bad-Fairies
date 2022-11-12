using _Scripts.Bots;
using UnityEngine;

namespace _Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        public Camera fpsCam;

        public int damage = 10;
        public float range = 100f;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {

                var enemy = hit.transform.GetComponent<BotController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
