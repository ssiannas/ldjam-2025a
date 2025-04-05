using UnityEngine;

namespace ldjam_hellevator
{
    public class FloaterController : MonoBehaviour
    {
        enum FloaterState
        {
            Idle,
            Moving
        }
        
        [SerializeField] FloaterState _state = FloaterState.Idle;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [Header("Floater Properties")] [SerializeField]
        private float speed = 5f;

        [SerializeField] private float movingCoeff = 2f; 
        
        [SerializeField] private Transform target;
        
        private Vector2 _moveDirection = Vector2.up; 
        private Rigidbody2D _rigidbody;
        private float minDistanceFromTarget = 5f;
        
        void Awake()
        {
             target = GameObject.FindGameObjectWithTag("Player").transform; 
             _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckState();
        }

        void CheckState()
        {
            var distance = Vector2.Distance(transform.position, target.position);
            _state = distance < minDistanceFromTarget ? FloaterState.Moving : FloaterState.Idle;
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
            }

        }

        void HandleIdleFixed()
        {
            _rigidbody.linearVelocity = Vector2.up * speed;
        }

        void HandleMovingFixed()
        {
            Vector2 lookAt = target.position - transform.position;
            var interpolatedPos = (Vector2.up + lookAt).normalized;
            _rigidbody.linearVelocity = interpolatedPos * (speed * movingCoeff);
        }
    }
}
