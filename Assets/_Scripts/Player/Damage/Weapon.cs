using _Scripts.Bots;
using _Scripts.Buildings;
using UnityEngine;
using UnityEngine.XR;

namespace _Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject bombPrefab;

        private int _damage;

        public Camera fpsCam;
        public LayerMask layerMask;

        // public int damage = 10;
        public float range = 100f;

        public float throwForce;

        void Start()
        {
            // _damage = player.GetComponent<PlayerController>().Stats.Damage;
        }

        // Update is called once per frame
        void Update()
        {
            _damage = player.GetComponent<Stats>().Damage;
            
            if (Input.GetButtonDown("Fire1") && player.GetComponent<PlayerController>().GetCurrentPlayerMode() == 0)
            {
                Debug.Log("Shoot");
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
                bomb.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * throwForce, ForceMode.Impulse);
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