using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;
    public float hurtForceX = 5f;
    public float hurtForceY = 4f;
    public float invincibleTime = 0.5f;

    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(Vector2 enemyPos)
    {
        if (!canTakeDamage) return;

        health--;

        Vector2 dir = ((Vector2)transform.position - enemyPos).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(dir.x * hurtForceX, hurtForceY), ForceMode2D.Impulse);

        Debug.Log("Player HP: " + health);

        StartCoroutine(Invincible());

        if (health <= 0)
        {
            Debug.Log("PLAYER DEAD");
        }
    }

    IEnumerator Invincible()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibleTime);
        canTakeDamage = true;
    }
}