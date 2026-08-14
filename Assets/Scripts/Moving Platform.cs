using System.Collections;
using UnityEngine;

/// <summary>
/// Moves back and forth between pointA and pointB. Requires a KINEMATIC Rigidbody —
/// see the Inspector setup notes. Uses MovePosition rather than velocity, which is
/// the correct way to move a kinematic body and avoids it being flung around by
/// physics collision response.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    public float waitTime = 1f;

    private Vector3 targetPosition;
    private bool isWaiting;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        if (!rb.isKinematic)
        {
            Debug.LogWarning($"{name}: MovingPlatform's Rigidbody should be marked Is Kinematic in the Inspector. Forcing it on at runtime.");
            rb.isKinematic = true;
        }

        if (pointA == null || pointB == null)
        {
            Debug.LogError("Moving Platform missing Point A or Point B!");
            enabled = false;
            return;
        }

        targetPosition = pointB.position;
    }

    private void FixedUpdate()
    {
        if (isWaiting) return;

        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, targetPosition) < 0.05f)
        {
            StartCoroutine(WaitAtEnd());
        }
    }

    private IEnumerator WaitAtEnd()
    {
        isWaiting = true;

        targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;

        yield return new WaitForSeconds(waitTime);

        isWaiting = false;
    }
}