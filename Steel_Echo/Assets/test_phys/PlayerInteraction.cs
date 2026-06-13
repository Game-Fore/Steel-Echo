using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform interactPoint;
    public float range = 0.6f;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(interactPoint.position, range, interactLayer);

            foreach (Collider2D hit in hits)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact();
                    break;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(interactPoint.position, range);
        }
    }
}