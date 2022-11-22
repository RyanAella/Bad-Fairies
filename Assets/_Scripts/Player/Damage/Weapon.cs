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
        [SerializeField] private float fireRate;

        private int _damage;
        private float _nextTimeToFire = 0f;

        public Camera fpsCam;
        public LayerMask layerMask;

        public float range = 100f;

        public float throwForce;

        void Update()
        {
            // Get the damage the player can make
            _damage = player.GetComponent<Stats>().Damage;
            
            // If Fire1 is pressen and player is in Default Mode
            if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && player.GetComponent<PlayerController>().GetPlayerMode() == PlayerController.PlayerMode.Default)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }

            // If R is pressed
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
                // If a building is hit
                if (hit.transform.gameObject.CompareTag("Buildable"))
                {
                    Debug.Log("Hit Buildable");
                    hit.transform.gameObject.GetComponent<Buildable>().TakeDamage(_damage);
                }

                // If a Bot (an enemy) is hit
                var enemy = hit.transform.GetComponent<BotController>();

                if (enemy != null)
                {
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }
}