using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float walkingSpeed = 12f;
        [SerializeField] private float runningSpeed = 24f;
        // [SerializeField] public float camSensitivity = 5.0f;
        // [SerializeField] public float camSmoothing = 2.0f;

        public Transform groundCheck;
        public float groundDistance = 0.5f;
        public LayerMask groundMask;
        
        private CharacterController _controller;
        private Vector3 _playerVelocity;

        private bool _isGrounded;
        
        private float _jumpHeight = 3f;
        private float _gravityValue = -9.81f;
        private float currentSpeed;

        void Start()
        {
            _controller = gameObject.AddComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
        
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
            }
            
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;

            if (_isGrounded && Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runningSpeed;
            }
            else
            {
                currentSpeed = walkingSpeed;
            }
            _controller.Move(move * currentSpeed * Time.deltaTime);
            
            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2f * _gravityValue);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
