using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float reachDistance = 0.1f;

    private Rigidbody2D rb;
    private Transform currentTarget;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB;
    }

    void FixedUpdate()
    {
        // Направление к текущей точке
        float direction = Mathf.Sign(currentTarget.position.x - transform.position.x);

        // Движение только по X
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        // Проверка достижения точки
        if (Mathf.Abs(transform.position.x - currentTarget.position.x) <= reachDistance)
        {
            // Переключаем цель
            currentTarget = currentTarget == pointA ? pointB : pointA;

            // Разворачиваем врага
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}