using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    [Header("Knockback")]
    public float knockbackForceX = 4f;
    public float knockbackForceY = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 hitPoint)
    {
        health -= damage;

        Vector2 dir = ((Vector2)transform.position - hitPoint).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(dir.x * knockbackForceX, knockbackForceY), ForceMode2D.Impulse);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}