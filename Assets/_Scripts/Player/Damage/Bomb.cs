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
            
            m_ExplosionInstance = Instantiate(explosionPrefab, pos, Quaternion.identity);
            gameObject.GetComponent<AudioSource>().Play();

            var objs = Physics.OverlapSphere(pos, explosionRadius, interactionLayer);
            foreach (var c in objs)
            {
                var dist = Vector3.Distance(pos, c.transform.position);

                var dmgMultiplier = Mathf.InverseLerp(explosionRadius, 0.0f, dist);
                Debug.Log("dmg multiplier: " + dmgMultiplier);

                var force = explosionForce * dmgMultiplier;
                var dmg = explosionDamage * dmgMultiplier;

                Rigidbody targetRigidbody = c.gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
                Stats targetStats = c.gameObject.GetComponent(typeof(Stats)) as Stats;

                if (targetRigidbody != null)
                {
                    Debug.Log("Bomb aplies force");
                    targetRigidbody.AddExplosionForce(force, pos, explosionRadius, 1, ForceMode.Impulse);
                }
                if (targetStats != null)
                {
                    Debug.Log("Bomb does damage");
                    targetStats.TakeDamage((int)dmg);
                }
            }
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            
            // Destroy(gameObject);
            Invoke(nameof(CleanUp), 2f);
        }

        void CleanUp()
        {
            Destroy(m_ExplosionInstance);
            Destroy(gameObject);
        }
    }
}