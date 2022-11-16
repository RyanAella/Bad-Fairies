using UnityEngine;

namespace _Scripts.Buildings
{
    public class Frame : MonoBehaviour
    {
        public Material frame; // visible
        public Material cutout; // invisible

        public bool tracked;

        public Buildable bc;

        // Update is called once per frame
        void Update()
        {
            GetComponent<Renderer>().material = tracked ? frame : cutout;

            tracked = false;
        }
    }
}