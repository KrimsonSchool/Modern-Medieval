using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 3.0f;

    private Vector3 targetPosition;

    void Start()
    {
        // Set the initial target to Point B
        if (pointB != null)
        {
            targetPosition = pointB.position;
        }
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        // Move the platform toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

        // Check if the platform has reached the target, then swap targets
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    // --- PLAYER RIDING LOGIC ---
    // Makes the player a child of the platform so they move with it.

    private void OnCollisionEnter(Collision collision)
    {
        // Adjust the tag check if your player uses a different tag (e.g., "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}