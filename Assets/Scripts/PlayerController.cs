using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ldjam_hellevator
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        [SerializeField] private float fallSpeed = 2f;
        [SerializeField] private float gravityIncreaseRate = 0.1f;
        private float moveInput;
        [SerializeField] private float moveSpeed = 5f;
        private Vector3 startPos;

        [Header("Channels")]
        [SerializeField] private ScoreManagerChannel scoreManagerChannel;
        [SerializeField] private UiManagerChannel uiManagerChannel;
        [SerializeField] private GmChannel gmChannel;
        [SerializeField] private AudioChannel audioChannel;
        
        [SerializeField] private int pointRate = 1;

        private SpriteRenderer _spriteRenderer;

        [Header("Health")]
        [SerializeField] private int maxLives = 3;
        public int currentLives { get; private set; }
        private bool isInvulnerable = false;
        [SerializeField] private float invulnerabilityDuration = 2f;

        [Header("Abilities")] 
        [SerializeField] private float dashCdSec = 3f;
        [SerializeField] private float dashForce = 10f;
        [SerializeField] private float dashDuration = 0.2f;
        
        private static readonly int IsDashing = Animator.StringToHash("IsDashing");
        private float _lastDashTime = -Mathf.Infinity;
        private bool _isDashing = false;
        private Vector3 _originalPosition;
        private bool _isReturning = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            startPos = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            currentLives = maxLives;
            _originalPosition = transform.position;
            gmChannel.BloomPulsate(4f, 4f, true);
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
            if (gmChannel == null)
            {
                throw new Exception("No GM Channel Assigned");
            }
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && Time.time - _lastDashTime > dashCdSec)
            {
                StartCoroutine(Dash());
            }
            moveInput = GetSteeringInput();
            MaybeFlipSprite(moveInput);
            MaybeHandleReturn();
        }

        private void FixedUpdate()
        {
            Steer();
            MaybeHandleReturnFixed();
        }

        private void MaybeHandleReturn()
        {
            if (!_isReturning) return;
            var yDifference = transform.position.y - _originalPosition.y;
            if (_isDashing || !(Mathf.Abs(yDifference) < 0.2f)) return;
            _isReturning = false;
            _animator.SetBool(IsDashing, false);
            _rigidbody.linearVelocityY = 0f;
        }

        private void MaybeHandleReturnFixed()
        {
            if (!_isReturning) return;
            _rigidbody.AddForce(Vector2.up * (dashForce * 5), ForceMode2D.Force);
 
        }

        private void MaybeFlipSprite(float moveInputHorizontal)
        {
            if (_spriteRenderer is null) return;
            _spriteRenderer.flipX = moveInputHorizontal < 0;
        }

        private void IncreaseGravity()
        {
            fallSpeed += gravityIncreaseRate * Time.deltaTime;
            _rigidbody.linearVelocityY = -1 * fallSpeed;
        }

        private float GetSteeringInput()
        {
            return Input.GetAxis("Horizontal");
        }

        private void Steer()
        {
            var velocity = _rigidbody.linearVelocity;
            velocity.x = moveInput * moveSpeed;
            _rigidbody.linearVelocityX = velocity.x;
        }

        public void TakeDamage()
        {
            if (isInvulnerable) return;
            currentLives--;
            
            Debug.Log("Player hit! Lives left: " + currentLives);
            audioChannel.PlayAudio(SoundNames.PlayerDamage);
            uiManagerChannel.UpdateHearts(currentLives);

            if (currentLives <= 0)
            {
                Die();
                Debug.Log("dead");
            }
            else
            {
                StartCoroutine(Invulnerability());
            }

            gmChannel.BloomPulsate(intensity: 14f, duration: 0.15f);
            gmChannel.CameraShake();

            if (currentLives <= 0)
            {
                Die();
                Debug.Log("dead");
            }
            else
            {
                StartCoroutine(Invulnerability());
            }
        }
        
        private IEnumerator Dash()
        {
            _lastDashTime = Time.time;
            _isDashing = true;
            _rigidbody.AddForce(Vector2.down * dashForce,  ForceMode2D.Impulse);
            uiManagerChannel.DashCooldown(dashCdSec);
            StartCoroutine(Invulnerability(duration: dashDuration*4, shouldFlash: false));
            _animator.SetBool(IsDashing, true);
            yield return new WaitForSeconds(dashDuration);
            _isReturning = true;
            _isDashing = false;
        }

        private void Die()
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }

        private System.Collections.IEnumerator Invulnerability(float duration = 2f, bool shouldFlash = true)
        {
            isInvulnerable = true;

            // Optional: Flash effect
            float flashDelay = 0.2f;
            for (float i = 0; i < duration; i += flashDelay)
            {
                if (_spriteRenderer is not null && shouldFlash)
                    _spriteRenderer.enabled = !_spriteRenderer.enabled;
                yield return new WaitForSeconds(flashDelay);
            }

            if (_spriteRenderer is not null && shouldFlash)
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
            else if (collision.gameObject.tag == "Demon1")
            {
                TakeDamage();
            }
        }

    }
}