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
            _controller = gameObject.GetComponent<CharacterController>();
        }

        void Update()
        {

            _isGrounded = _controller.isGrounded; //Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            _controller.slopeLimit = 50;
            
            // 
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
            }
            
            float xAxis = Input.GetAxis("Horizontal");
            float zAxis = Input.GetAxis("Vertical");
            Vector3 move = transform.right * xAxis + transform.forward * zAxis;

            // Player is grounded and left shift is pressed
            if (_isGrounded && Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runningSpeed;
            }
            else
            {
                currentSpeed = walkingSpeed;
            }
            _controller.Move(move * currentSpeed * Time.deltaTime);
            
            // Changes the height position of the player
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2f * _gravityValue);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
