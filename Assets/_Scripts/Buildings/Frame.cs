using UnityEngine;

namespace _Scripts.Buildings
{
    public class Frame : MonoBehaviour
    {
        public Material frame; // visible
        public Material cutout; // invisible

        public bool tracked;

        public Buildable bc;

        void Update()
        {
            if (!GameManager._gamePaused)
            {
                // If the player looks at the frame the color is set to visible
                GetComponent<Renderer>().material = tracked ? frame : cutout;

                tracked = false;
            }
        }
    }
}