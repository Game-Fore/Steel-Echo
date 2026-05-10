using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public float damageCooldown = 1f;
    private bool canDamage = true;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!canDamage) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(transform.position);
                StartCoroutine(DamageDelay());
            }
        }
    }

    IEnumerator DamageDelay()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}