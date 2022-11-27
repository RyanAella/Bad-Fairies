using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float walkingSpeed = 6f;
        [SerializeField] private float runningSpeed = 12f;
        [SerializeField] private float jumpHeight = 3f;

        [SerializeField] private float gravityValue = -9.81f;

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
            if (!GameManager._gamePaused)
            {
                var playerMode = gameObject.GetComponent<PlayerController>().GetPlayerMode();

                // Check the player grounded state and set the slope limit
                _isGrounded = _controller.isGrounded;
                //_controller.slopeLimit = 50;

                // If the player is grounded
                if (_isGrounded && _playerVelocity.y < 0)
                {
                    _playerVelocity.y = -2f;
                }

                var xAxis = Input.GetAxis("Horizontal");
                var zAxis = Input.GetAxis("Vertical");
                var moveDir = transform.right * xAxis + transform.forward * zAxis;

                // Player is grounded and left shift is pressed and player is in Default Mode
                if (_isGrounded && Input.GetKey(KeyCode.LeftShift) && playerMode == PlayerController.PlayerMode.Default)
                {
                    _currentSpeed = runningSpeed;
                }
                else
                {
                    _currentSpeed = walkingSpeed;
                }

                _controller.Move(_currentSpeed * Time.deltaTime * moveDir);

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
}