using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        // the first person camera
        public new GameObject camera;

        [SerializeField] private float mouseSensitivity = 100f;

        private float _xRotation;

        void Start()
        {
            camera = transform.Find("fp_camera").gameObject;
        }

        void Update()
        {
            if (!GameManager._gamePaused)
            {
                // camera

                var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                _xRotation -= mouseY;
                _xRotation = Mathf.Clamp(_xRotation, -70f, 60f);

                transform.Rotate(Vector3.up * mouseX);
                camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            }
        }
    }
}