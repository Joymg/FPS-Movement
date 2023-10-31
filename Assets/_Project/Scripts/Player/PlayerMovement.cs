using UnityEngine;

namespace Joymg
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform orientation;
        private Rigidbody rb;

        [Header("Movement Variables")]
        [SerializeField]
        private float movementSpeed;
        private float horizontalInput;
        private float verticalInput;
        private Vector3 movementDirection;

        [Header("Ground Check")]
        [SerializeField]
        private LayerMask groundLayer;
        private float playerHeight = 2f;
        [SerializeField]
        private bool isGrounded;

        [SerializeField]
        private float groundDrag;

        [Header("Jump Variables")]
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float maxJumps;
        [SerializeField]
        private float numJumps;
        [SerializeField]
        private float airMultiplier;


        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            numJumps = maxJumps;
        }

        private void Update()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

            if (isGrounded && numJumps != maxJumps)
            {
                ResetJumps();
            }

            HandleInput();
            ClampSpeed();

            if (isGrounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
            {
                HandleJump();
            }
        }

        private void HandleMovement()
        {
            movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (isGrounded)
            {
                rb.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
            }
            else
            {
                rb.AddForce(movementDirection.normalized * movementSpeed * airMultiplier, ForceMode.Force);
            }
        }

        private void ClampSpeed()
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVelocity.sqrMagnitude > movementSpeed * movementSpeed)
            {
                Vector3 clampedSpeed = flatVelocity.normalized * movementSpeed;
                rb.velocity = new Vector3(clampedSpeed.x, 0f, clampedSpeed.z);
            }
        }

        private void HandleJump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            numJumps--;
        }

        private void ResetJumps()
        {
            numJumps = maxJumps;
        }
    }
}
