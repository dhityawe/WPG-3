using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG_2DExplore {
    public class boatMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float rotationSpeed = 200f;
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float moveInput = Input.GetAxis("Vertical");
            float rotationInput = Input.GetAxis("Horizontal");

            Vector2 moveDirection = transform.up * moveInput * speed * Time.deltaTime;
            rb.MovePosition(rb.position + moveDirection);

            float rotation = rotationInput * rotationSpeed * Time.deltaTime;
            rb.MoveRotation(rb.rotation - rotation);
        }
    }
}