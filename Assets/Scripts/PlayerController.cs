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

        [Header("Channels")]
        [SerializeField] private ScoreManagerChannel scoreManagerChannel;
        [SerializeField] private UiManagerChannel uiManagerChannel;
        [SerializeField] private int pointRate = 1;

        private SpriteRenderer _spriteRenderer;

        [Header("Health")]
        [SerializeField] private int maxLives = 3;
        public int currentLives { get; private set; }
        private bool isInvulnerable = false;
        [SerializeField] private float invulnerabilityDuration = 2f;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startPos = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            currentLives = maxLives;
        }

        private void Awake()
        {
            if (scoreManagerChannel == null)
            {
                throw new Exception("No Score Manager Channel Assigned");
            }
            if (uiManagerChannel == null)
            {
                throw new Exception("No UI Manager Channel Assigned");
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


        public void TakeDamage()
        {
            //if (isInvulnerable) return;

            currentLives--;
            Debug.Log("Player hit! Lives left: " + currentLives);

            uiManagerChannel.UpdateHearts(currentLives);

            if (currentLives <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Invulnerability());
            }
        }

        private void Die()
        {
            Debug.Log("Player died!");
            gameObject.SetActive(false);
        }

        private System.Collections.IEnumerator Invulnerability()
        {
            isInvulnerable = true;

            // Optional: Flash effect
            float flashDelay = 0.2f;
            for (float i = 0; i < invulnerabilityDuration; i += flashDelay)
            {
                if (_spriteRenderer != null)
                    _spriteRenderer.enabled = !_spriteRenderer.enabled;
                yield return new WaitForSeconds(flashDelay);
            }

            if (_spriteRenderer != null)
                _spriteRenderer.enabled = true;

            isInvulnerable = false;
        }

        private void OnTriggerEnter2D(Collider2D collision) 
        {
            if (isInvulnerable) return;
            if (collision.gameObject.tag == "Bullet")
            {
                Destroy(collision.gameObject);
                TakeDamage();
            }
            else if (collision.gameObject.tag == "Pillar")
            {
                TakeDamage();
            }
        }

    }
}