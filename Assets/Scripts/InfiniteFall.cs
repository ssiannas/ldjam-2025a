using UnityEngine;

public class InfiniteFall : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float gravityIncreaseRate = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.gravityScale += gravityIncreaseRate * Time.deltaTime;
    }
}
