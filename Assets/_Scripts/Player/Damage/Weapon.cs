using _Scripts.Bots;
using _Scripts.Buildings;
using UnityEngine;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;

namespace _Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private float fireRate;
        [SerializeField] private bool fullAuto = true;

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

            // if player in default mode enable weapons
            if (player.GetComponent<PlayerController>().GetPlayerMode() == PlayerController.PlayerMode.Default)
            {
                // If Fire1 is pressed and next time to fire matches then fire

                if (fullAuto)
                {
                    if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
                    {
                        _nextTimeToFire = Time.time + 1f / fireRate;
                        Shoot();
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToFire)
                    {
                        _nextTimeToFire = Time.time + 1f / fireRate;
                        Shoot();
                    }
                }
                

                // If R is pressed
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
                    bomb.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * throwForce, ForceMode.Impulse);
                }
            }
        }

        void Shoot()
        {
            // trigger shoot effect
            var shootEffect = transform.GetChild(0).GetComponent(typeof(ParticleSystem)) as ParticleSystem;
            shootEffect.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
            {
                //var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);

                // Instantiate the explosion at the weapon position
                //_shotInstance = Instantiate(shotPrefab, pos, Quaternion.identity);

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