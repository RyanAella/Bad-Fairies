using UnityEngine;

namespace _Scripts
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private float explosionRadius;
        [SerializeField] private LayerMask interactionLayer;

        [SerializeField] private float explosionForce;
        [SerializeField] private float explosionDamage;

        public GameObject explosionPrefab;        

        private GameObject m_ExplosionInstance;

        void Start()
        {
            // Start Explosion routine
            Invoke(nameof(Explode), delay);
        }

        void Explode()
        {
            var pos = transform.position;
            
            // Instantiate the explosion at the bomb position and play the Audio
            m_ExplosionInstance = Instantiate(explosionPrefab, pos, Quaternion.identity);
            gameObject.GetComponent<AudioSource>().Play();

            // Check if an Object with a certain Layer is inside the explosionRadius
            var objs = Physics.OverlapSphere(pos, explosionRadius, interactionLayer);
            foreach (var c in objs)
            {
                // Get the distance from bomb to object
                var dist = Vector3.Distance(pos, c.transform.position);

                // Get the damage multiplier depending on the distance
                var dmgMultiplier = Mathf.InverseLerp(explosionRadius, 0.0f, dist);

                // Multiply the explosionForce/explosionDamage with the damage multiplier
                var force = explosionForce * dmgMultiplier;
                var dmg = explosionDamage * dmgMultiplier;

                Rigidbody targetRigidbody = c.gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
                Stats targetStats = c.gameObject.GetComponent(typeof(Stats)) as Stats;

                // If the target has a Rigidbody
                if (targetRigidbody != null)
                {
                    targetRigidbody.AddExplosionForce(force, pos, explosionRadius, 1, ForceMode.Impulse);
                }
                // If the target has life
                if (targetStats != null)
                {
                    targetStats.TakeDamage((int)dmg);
                }
            }
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            
            // Destroy(gameObject);
            Invoke(nameof(CleanUp), 2f);
        }

        // Destroy the bomb and the explosion
        void CleanUp()
        {
            Destroy(m_ExplosionInstance);
            Destroy(gameObject);
        }
    }
}