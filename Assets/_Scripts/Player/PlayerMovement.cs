using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float walkingSpeed = 12f;
        [SerializeField] private float runningSpeed = 24f;
        [SerializeField] private float jumpHeight = 3f;

        [SerializeField] private float gravityValue = -9.81f;
        // [SerializeField] public float camSensitivity = 5.0f;
        // [SerializeField] public float camSmoothing = 2.0f;

        // public Transform groundCheck;
        // public float groundDistance = 0.5f;
        // public LayerMask groundMask;

        private CharacterController _controller;
        private Vector3 _playerVelocity;

        private bool _isGrounded;

        private float _currentSpeed;

        void Start()
        {
            _controller = gameObject.GetComponent<CharacterController>();
        }

        void Update()
        {
            var playerMode = gameObject.GetComponent<PlayerController>().GetCurrentPlayerMode();

            _isGrounded =
                _controller.isGrounded; //Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            _controller.slopeLimit = 50;

            // 
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
            }

            var xAxis = Input.GetAxis("Horizontal");
            var zAxis = Input.GetAxis("Vertical");
            var move = transform.right * xAxis + transform.forward * zAxis;

            // Player is grounded and left shift is pressed
            if (_isGrounded && Input.GetKey(KeyCode.LeftShift) && playerMode == 0)
            {
                _currentSpeed = runningSpeed;
            }
            else
            {
                _currentSpeed = walkingSpeed;
            }

            _controller.Move(move * _currentSpeed * Time.deltaTime);

            // Changes the height position of the player
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            }

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}