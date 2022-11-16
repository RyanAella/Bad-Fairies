using _Scripts.Bots;
using _Scripts.Buildings;
using UnityEngine;

namespace _Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        
        public Camera fpsCam;
        public LayerMask layerMask;

        public int damage = 10;
        public float range = 100f;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1") && player.GetComponent<PlayerController>().GetCurrentPlayerMode() == 0)
            {
                Debug.Log("Shoot");
                Shoot();
            }
        }

        void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
            {
                if (hit.transform.gameObject.CompareTag("Buildable"))
                {
                    hit.transform.gameObject.GetComponent<Buildable>().TakeDamage(damage);
                }

                var enemy = hit.transform.GetComponent<BotController>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}