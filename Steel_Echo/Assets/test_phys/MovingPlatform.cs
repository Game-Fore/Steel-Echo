using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    public bool isActive = false;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void FixedUpdate()
    {
        if (!isActive) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}