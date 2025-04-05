using System;
using UnityEngine;

namespace ldjam_hellevator
{
    public class Bullets : MonoBehaviour
    {
        [Header("Bullet Properties")]
        [SerializeField] private float speed = 10f;
        private Rigidbody2D _rb;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.linearVelocity = Vector2.up * speed;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void FixedUpdate()
        {
            
        }
    }
}
