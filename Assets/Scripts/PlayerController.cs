using System;
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
        
        [SerializeField]
        private ScoreManagerChannel scoreManagerChannel;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startPos = transform.position;
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
            //IncreaseGravity();
            moveInput = GetStreeringInput();
            IncreaseScore();
        }


        void FixedUpdate()
        {
            Steer();
        }


        void IncreaseGravity()
        {
            fallSpeed += gravityIncreaseRate * Time.deltaTime;
            rb.linearVelocityY = -1 * fallSpeed;
            //transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }

        float GetStreeringInput()
        {
            return Input.GetAxis("Horizontal");
        }

        void Steer()
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.x = moveInput * moveSpeed;
            rb.linearVelocityX = velocity.x;
        }

        void IncreaseScore()
        {
            scoreManagerChannel.SetScore(Mathf.RoundToInt(Mathf.Abs(transform.position.y - startPos.y)));
        }
    }
}