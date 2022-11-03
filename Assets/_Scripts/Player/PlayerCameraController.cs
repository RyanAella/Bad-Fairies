using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        // the first person camera
        public GameObject camera;
        public float mouseSensitivity = 100f;
        private float xRotation = 0f;
        
        void Start()
        {
            
            camera = transform.Find("fp_camera").gameObject;
        }

        void Update()
        {
            // camera

            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -70f, 60f);
            
            transform.Rotate(Vector3.up * mouseX);
            camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}