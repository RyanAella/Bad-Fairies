using _Scripts.Bots;
using _Scripts.Buildings;
using UnityEngine;

namespace _Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private int _damage;

        public Camera fpsCam;
        public LayerMask layerMask;

        // public int damage = 10;
        public float range = 100f;

        void Start()
        {
            // _damage = player.GetComponent<PlayerController>().Stats.Damage;
        }

        // Update is called once per frame
        void Update()
        {
            _damage = player.GetComponent<PlayerController>().Stats.Damage;
            
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
                    hit.transform.gameObject.GetComponent<Buildable>().TakeDamage(_damage);
                }

                var enemy = hit.transform.GetComponent<BotController>();

                if (enemy != null)
                {
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }
}