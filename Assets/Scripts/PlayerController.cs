using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private float gravityIncreaseRate = 0.1f;
    private float moveInput;
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        increaseGravity();
        moveInput = getStreeringInput();
        increaseScore();
    }


    void FixedUpdate()
    {
        steer();
    }


    void increaseGravity() 
    {
        fallSpeed += gravityIncreaseRate * Time.deltaTime;
        rb.linearVelocityY = -1*fallSpeed;
        //transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    float getStreeringInput()
    {
        return Input.GetAxis("Horizontal");
    }

    void steer()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;
        rb.linearVelocityX = velocity.x;
    }

    void increaseScore()
    {
        ScoreManager.Instance.SetScore(Mathf.RoundToInt(Mathf.Abs(transform.position.y - startPos.y)));
    }
}
