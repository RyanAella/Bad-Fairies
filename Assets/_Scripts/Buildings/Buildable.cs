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
        // public GameObject frameWall5;
        // public GameObject frameWall6;
        // public GameObject frameWall7;
        // public GameObject frameWall8;

        private Stats m_stats;

        private void Start()
        {
            m_stats = GetComponent(typeof(Stats)) as Stats;
        }
        
        private void Update()
        {
            switch (mode)
            {
                // Floor building mode
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
                    // frameWall5.SetActive(false);
                    // frameWall6.SetActive(false);
                    // frameWall7.SetActive(false);
                    // frameWall8.SetActive(false);
                    break;
                // Ramp building mode
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
                    // frameWall5.SetActive(false);
                    // frameWall6.SetActive(false);
                    // frameWall7.SetActive(false);
                    // frameWall8.SetActive(false);
                    break;
                // Wall building mode
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
                    // frameWall5.SetActive(true);
                    // frameWall6.SetActive(true);
                    // frameWall7.SetActive(true);
                    // frameWall8.SetActive(true);
                    break;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                transform.parent.transform.parent = null;          // The container is needed...
            }
        }
        
        public void TakeDamage(int dmg)
        {
            if (m_stats.CurrentHealth <= 0)
            {
                Destroy(gameObject.transform.parent);
            }
            else
            {
               m_stats.TakeDamage(dmg); 
            }
        }
    }
}
