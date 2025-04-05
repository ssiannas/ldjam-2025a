using System;
using JetBrains.Annotations;
using UnityEngine;

namespace ldjam_hellevator
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private float fallSpeed = 2f;
        [SerializeField] private float gravityIncreaseRate = 0.1f;
        private float moveInput;
        [SerializeField] private float moveSpeed = 5f;
        private Vector3 startPos;

        [SerializeField] private ScoreManagerChannel scoreManagerChannel;
        [SerializeField] private int pointRate = 1;

        private SpriteRenderer _spriteRenderer;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startPos = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            if (scoreManagerChannel == null)
            {
                throw new Exception("No Score Manager Channel Assigned");
            }
        }

        // Update is called once per frame
        void Update()
        {
            moveInput = GetSteeringInput();
            MaybeFlipSprite(moveInput);
        }

        private void FixedUpdate()
        {
            Steer();
        }

        private void MaybeFlipSprite(float moveInputHorizontal)
        {
            if (_spriteRenderer is null) return;
            _spriteRenderer.flipX = moveInputHorizontal < 0;
        }

        private void IncreaseGravity()
        {
            fallSpeed += gravityIncreaseRate * Time.deltaTime;
            rb.linearVelocityY = -1 * fallSpeed;
        }

        private float GetSteeringInput()
        {
            return Input.GetAxis("Horizontal");
        }

        private void Steer()
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.x = moveInput * moveSpeed;
            rb.linearVelocityX = velocity.x;
        }
    }
}