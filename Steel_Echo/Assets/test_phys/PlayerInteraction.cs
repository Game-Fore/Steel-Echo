using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float range = 1.5f;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, range, interactLayer);

            if (hit != null)
            {
                hit.GetComponent<IInteractable>()?.Interact();
            }
        }
    }
}