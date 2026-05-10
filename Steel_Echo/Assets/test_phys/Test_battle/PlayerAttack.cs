using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayer;
    public int damage = 1;
    public float recoilForce = 2f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth hp = enemy.GetComponent<EnemyHealth>();

            if (hp != null)
            {
                hp.TakeDamage(damage, attackPoint.position);

                HitStopManager.instance.Stop(0.05f);

                Vector2 recoilDir = ((Vector2)transform.position - (Vector2)enemy.transform.position).normalized;
                rb.AddForce(new Vector2(recoilDir.x * recoilForce, 1f), ForceMode2D.Impulse);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}