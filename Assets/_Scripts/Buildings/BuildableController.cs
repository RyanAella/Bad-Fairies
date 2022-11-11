using System;
using UnityEngine;

namespace _Scripts.Buildings
{
    public class BuildableController : MonoBehaviour
    {
        public int modus = 0;
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
        
        private void Start()
        {
        }

        private void Update()
        {
            switch (modus)
            {
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
}
