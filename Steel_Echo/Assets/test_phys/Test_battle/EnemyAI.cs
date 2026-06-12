using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
        Dead
    }

    [Header("State")]
    public EnemyState currentState = EnemyState.Patrol;

    [Header("References")]
    public Transform pointA;
    public Transform pointB;
    public Transform player;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float reachDistance = 0.1f;

    [Header("Detection")]
    public float chaseDistance = 5f;
    private Rigidbody2D rb;
    private Transform currentTarget;
    private bool facingRight = true;
    [Header("Attack")]
    public float attackDistance = 1.5f;
    public float attackCooldown = 1f;
    private float attackTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (currentState == EnemyState.Dead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        UpdateState();

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    void UpdateState()
    {
        if (player == null) return;

        float distanceToPlayer =
            Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
            currentState = EnemyState.Attack;
        else if (distanceToPlayer <= chaseDistance)
            currentState = EnemyState.Chase;
        else
            currentState = EnemyState.Patrol;
    }

    void Patrol()
    {
        float direction =
            Mathf.Sign(currentTarget.position.x - transform.position.x);

        rb.linearVelocity =
            new Vector2(direction * patrolSpeed, rb.linearVelocity.y);

        if (Mathf.Abs(transform.position.x - currentTarget.position.x)
            <= reachDistance)
        {
            currentTarget =
                currentTarget == pointA ? pointB : pointA;

            FaceDirection(direction);
        }
    }

    void Chase()
    {
        float direction =
            Mathf.Sign(player.position.x - transform.position.x);

        rb.linearVelocity =
            new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

        FaceDirection(direction);
    }
    void Attack()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        if (player == null) return;

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        FaceDirection(direction);

        attackTimer -= Time.fixedDeltaTime;

        if (attackTimer <= 0f)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(transform.position);
            }

            attackTimer = attackCooldown;
        }
    }
    void FaceDirection(float direction)
    {
        if (direction > 0 && !facingRight)
            Flip();
        else if (direction < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Die()
    {
        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.Die();
        }
        currentState = EnemyState.Dead;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }



}
