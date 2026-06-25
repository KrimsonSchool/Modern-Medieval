using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA;
    public Transform pointB;

    public float speed = 3f;
    public float waitTime = 1f;

    private Vector3 targetPosition;
    private bool isWaiting;

    private void Start()
    {
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

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            StartCoroutine(WaitAtEnd());
        }
    }

    private IEnumerator WaitAtEnd()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);

        if (targetPosition == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else
        {
            targetPosition = pointA.position;
        }

        isWaiting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
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