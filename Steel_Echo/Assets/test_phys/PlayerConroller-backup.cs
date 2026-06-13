//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public float speed = 5f;
//    public float jumpForce = 10f;

//    private Rigidbody2D rb;
//    private bool isGrounded;

//    public Transform groundCheck;
//    public float checkRadius = 0.2f;
//    public LayerMask groundLayer;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }
//    void Move()
//    {
//        float move = Input.GetAxis("Horizontal");
//        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
//    }
//    void Jump()
//    {
//        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

//        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//        {
//            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
//        }
//    }
//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Platform"))
//        {
//            transform.SetParent(collision.transform);
//        }
//    }

//    void OnCollisionExit2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Platform"))
//        {
//            transform.SetParent(null);
//        }
//    }

//    private void Update()
//    {
//        Move();
//        Jump();
//    }
//}
