using UnityEngine;

namespace _Scripts.Buildings
{
    public class Buildable : MonoBehaviour
    {
        public int mode;

        public GameObject frameFloor1;
        public GameObject frameFloor2;
        public GameObject frameFloor3;
        public GameObject frameFloor4;
        public GameObject frameRamp1;
        public GameObject frameRamp2;
        public GameObject frameRamp3;
        public GameObject frameRamp4;
        public GameObject frameWall1;
        public GameObject frameWall2;
        public GameObject frameWall3;
        public GameObject frameWall4;

        private Stats m_stats;

        private void Start()
        {
            m_stats = GetComponent(typeof(Stats)) as Stats;
        }

        private void Update()
        {
            if (!GameManager._gamePaused)
            {
                switch (mode)
                {
                    // Floor building mode
                    // Set floor frames to active
                    case 0:
                        frameFloor1.SetActive(true);
                        frameFloor2.SetActive(true);
                        frameFloor3.SetActive(true);
                        frameFloor4.SetActive(true);
                        frameRamp1.SetActive(false);
                        frameRamp2.SetActive(false);
                        frameRamp3.SetActive(false);
                        frameRamp4.SetActive(false);
                        frameWall1.SetActive(false);
                        frameWall2.SetActive(false);
                        frameWall3.SetActive(false);
                        frameWall4.SetActive(false);
                        break;
                    // Ramp building mode
                    // Set ramp frames to active
                    case 1:
                        frameFloor1.SetActive(false);
                        frameFloor2.SetActive(false);
                        frameFloor3.SetActive(false);
                        frameFloor4.SetActive(false);
                        frameRamp1.SetActive(true);
                        frameRamp2.SetActive(true);
                        frameRamp3.SetActive(true);
                        frameRamp4.SetActive(true);
                        frameWall1.SetActive(false);
                        frameWall2.SetActive(false);
                        frameWall3.SetActive(false);
                        frameWall4.SetActive(false);
                        break;
                    // Wall building mode
                    // Set wall frames to active
                    case 2:
                        frameFloor1.SetActive(false);
                        frameFloor2.SetActive(false);
                        frameFloor3.SetActive(false);
                        frameFloor4.SetActive(false);
                        frameRamp1.SetActive(false);
                        frameRamp2.SetActive(false);
                        frameRamp3.SetActive(false);
                        frameRamp4.SetActive(false);
                        frameWall1.SetActive(true);
                        frameWall2.SetActive(true);
                        frameWall3.SetActive(true);
                        frameWall4.SetActive(true);
                        break;
                }
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            // If the buildable is placed on the ground its container doesn't have a parent 
            if (collision.gameObject.CompareTag("Ground"))
            {
                transform.parent.transform.parent = null;
            }
        }

        public void TakeDamage(int dmg)
        {
            // If life is equal to or smaller than 0 the container gets destroyed
            if (m_stats.CurrentHealth <= 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            // Otherwise it takes damage
            else
            {
                m_stats.TakeDamage(dmg);
            }
        }
    }
}
