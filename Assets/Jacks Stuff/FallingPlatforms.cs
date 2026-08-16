using System.Collections;
using UnityEngine;

/// <summary>
/// Platform that shakes as a warning, then falls once the player has stood on it
/// too long, and respawns back at its start position after a delay � Crash Bandicoot style.
/// Requires a Rigidbody (any mass) and a Collider on this object, and the player's
/// collider/rigidbody must be tagged "Player".
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class FallingPlatform : MonoBehaviour
{
    [Header("Timing")] [Tooltip("How long the player can stand on the platform before it starts to fall.")] [SerializeField]
    private float fallDelay = 1.5f;

    [Tooltip("How long the platform stays 'fallen' before it resets.")] [SerializeField]
    private float respawnDelay = 3f;

    [Header("Warning Shake")] [SerializeField]
    private float shakeIntensity = 0.03f;

    [Header("Respawn Visuals")] [Tooltip("Hide the platform completely while it's away, like a Crash box popping back in.")] [SerializeField]
    private bool hideWhileRespawning = true;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;
    private Collider col;
    private Renderer[] renderers;

    private bool isTriggered;
    private bool isFalling;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        // Starts kinematic so it just sits there solidly until triggered.
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryTrigger(collision.gameObject);
    }

    // Use this instead of OnCollisionEnter if your platform's collider is a trigger.
    private void OnTriggerEnter(Collider other)
    {
        TryTrigger(other.gameObject);
    }

    private void TryTrigger(GameObject obj)
    {
        if (isTriggered || isFalling) return;
        if (!obj.CompareTag("Player")) return;

        isTriggered = true;
        StartCoroutine(FallSequence());
    }

    private IEnumerator FallSequence()
    {
        if (GetComponent<PlatformBob>() != null)
        {
            GetComponent<PlatformBob>().enabled = false;
        }

        Vector3 restPosition = transform.position;
        float timer = 0f;

        // Warning shake so the player has a fair chance to jump off.
        while (timer < fallDelay)
        {
            timer += Time.deltaTime;
            float shakeX = Random.Range(-shakeIntensity, shakeIntensity);
            float shakeZ = Random.Range(-shakeIntensity, shakeIntensity);
            transform.position = restPosition + new Vector3(shakeX, 0f, shakeZ);
            yield return null;
        }

        transform.position = restPosition;

        // Let physics take over and drop the platform.
        isFalling = true;
        rb.isKinematic = false;

        print("making kinematic");
        yield return new WaitForSeconds(respawnDelay);

        print(rb.isKinematic);
        RespawnPlatform();
    }

    private void RespawnPlatform()
    {
        if (GetComponent<PlatformBob>() != null)
        {
            GetComponent<PlatformBob>().enabled = true;
        }

        if (hideWhileRespawning)
        {
            SetVisible(false);
        }

        rb.linearVelocity = Vector3.zero; // Unity 6+. If you're on an older Unity version, use rb.velocity instead.
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.position = startPosition;
        transform.rotation = startRotation;

        isTriggered = false;
        isFalling = false;

        if (hideWhileRespawning)
        {
            SetVisible(true); // pop back in, ready to stand on again
        }
    }

    private void SetVisible(bool visible)
    {
        foreach (var r in renderers)
        {
            r.enabled = visible;
        }

        col.enabled = visible;
    }
}