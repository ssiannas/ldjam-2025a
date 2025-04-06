using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ldjam_hellevator
{
    public class FloaterController : MonoBehaviour
    {
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        enum FloaterState
        {
            Idle,
            Moving,
            PostAttack
        }
        
        [SerializeField] FloaterState _state = FloaterState.Idle;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [Header("Floater Properties")] [SerializeField]
        private float speed = 5f;

        [SerializeField] private float movingCoeff = 1.2f; 
        [SerializeField] private Transform target;
        
        private Vector2 _moveDirection = Vector2.up; 
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        // Maybe add random offset here in awake
        private readonly float _minDistanceFromTarget = 7f;
        [SerializeField] private float _waitDurationSec = 1.5f;
        
        void Awake()
        {
             target = GameObject.FindGameObjectWithTag("Player").transform; 
             _rigidbody = GetComponent<Rigidbody2D>();
             _animator = GetComponent<Animator>();
             _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckState();
            MaybeFlipSprite();
        }

        void MaybeFlipSprite()
        {
            var direction = (target.position - transform.position).normalized;
            if (_spriteRenderer is null) return;
            _spriteRenderer.flipX = direction.x < 0;
        }

        void CheckState()
        { 
            var distance = Vector3.Distance(transform.position, target.position);
            switch (_state)
            {
                case FloaterState.Idle: 
                    var shouldMove = distance < _minDistanceFromTarget && _state != FloaterState.Moving;
                    var newState = shouldMove ? FloaterState.Moving : FloaterState.Idle;
                    
                    if (newState != _state)
                    {
                        StartCoroutine(StartMoving());
                    }
                    break;

                case FloaterState.PostAttack:
                    break;
            }
        }
        
        void FixedUpdate()
        {
            switch (_state)
            {
                case FloaterState.Idle:
                    HandleIdleFixed();
                    break;
                case FloaterState.Moving:
                    HandleMovingFixed();
                    break;
                case FloaterState.PostAttack:
                    HandlePostAttackFixed();
                    break;
            }
        }
        
        IEnumerator StartMoving()
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _moveDirection = Vector2.zero;
            _state = FloaterState.Moving;
            var targetDirection = (target.position - transform.position).normalized; 
            yield return new WaitForSeconds(_waitDurationSec);
            _moveDirection = targetDirection;
            _animator.SetBool(IsAttacking, true);
            StartCoroutine(StartPostAttacking());
        }

        IEnumerator StartPostAttacking()
        {
            yield return new WaitForSeconds(_waitDurationSec);
            _state = FloaterState.PostAttack;
        }

        void HandleIdleFixed()
        {
            _rigidbody.linearVelocity = Vector2.up * speed;
        }

        void HandleMovingFixed()
        {
            _rigidbody.linearVelocity = _moveDirection * (speed * movingCoeff);
        }

        void HandlePostAttackFixed()
        {
            _rigidbody.linearVelocity = Vector2.up * speed;
        }
    }
}
