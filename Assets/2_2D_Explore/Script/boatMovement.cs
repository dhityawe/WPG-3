using UnityEngine;

namespace MG_2DExplore {
    public class boatMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float rotationSpeed = 200f;
        private Rigidbody2D rb;
        private Animator animator;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float moveInput = Input.GetAxis("Vertical");
            float rotationInput = Input.GetAxis("Horizontal");

            Vector2 moveDirection = transform.up * moveInput * speed * Time.deltaTime;
            rb.MovePosition(rb.position + moveDirection);

            float rotation = rotationInput * rotationSpeed * Time.deltaTime;
            rb.MoveRotation(rb.rotation - rotation);

            // Update animation state
            bool IsMoving = moveInput != 0 || rotationInput != 0;
            animator.SetBool("IsMoving", IsMoving);
        }
    }
}