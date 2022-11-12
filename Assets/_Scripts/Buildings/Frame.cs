using UnityEngine;

namespace _Scripts.Buildings
{
    public class Frame : MonoBehaviour
    {
        public Material frame;   // visible
        public Material cutout;   // invisible

        public bool tracked;

        public Buildable bc;

        // Update is called once per frame
        void Update()
        {
            if (tracked)
            {
                this.GetComponent<Renderer>().material = frame;
            }
            else
            {
                this.GetComponent<Renderer>().material = cutout;
            }

            tracked = false;
        }
    }
}
