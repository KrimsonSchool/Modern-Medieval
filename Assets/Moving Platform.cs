using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")] public Transform pointA;
    public Transform pointB;

    public float speed = 3f;
    public float waitTime = 1f;

    private Vector3 targetPosition;
    private bool isWaiting;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

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
        if (!isWaiting)
        {
            Vector3 direction = (targetPosition - rb.position).normalized;
            rb.linearVelocity = direction * speed;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                StartCoroutine(WaitAtEnd());
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private IEnumerator WaitAtEnd()
    {
        isWaiting = true;
        
        rb.linearVelocity = Vector3.zero;
        
        targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;

        yield return new WaitForSeconds(waitTime);

        isWaiting = false;
    }
}