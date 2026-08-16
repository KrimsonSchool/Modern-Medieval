using UnityEngine;


public class PlatformBob : MonoBehaviour
{
    [Header("Bob Settings")]
    [Tooltip("How far up/down the platform moves from its resting position.")]
    [SerializeField] private float bobHeight = 0.25f;

    [Tooltip("How fast the platform bobs. Higher = faster.")]
    [SerializeField] private float bobSpeed = 1.5f;

    private Rigidbody rb;
    private Vector3 basePosition;
    private float timeOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null && !rb.isKinematic)
        {
            Debug.LogWarning($"{name}: PlatformBob's Rigidbody should be marked Is Kinematic in the Inspector. Forcing it on at runtime.");
            rb.isKinematic = true;
        }

        SetBaseFromCurrentPosition();

        // Random offset so multiple platforms don't all bob in perfect sync.
        timeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void FixedUpdate()
    {
        float y = Mathf.Sin(Time.time * bobSpeed + timeOffset) * bobHeight;
        Vector3 pos = basePosition;
        pos.y += y;

        if (rb != null)
        {
            rb.MovePosition(pos);
        }
        else
        {
            transform.position = pos;
        }
    }

    /// <summary>
    /// Call this whenever the platform's resting position changes —
    /// e.g. right after a FallingPlatform respawn resets its transform.
    /// Without this, the platform would try to bob around its old position.
    /// </summary>
    public void SetBaseFromCurrentPosition()
    {
        basePosition = rb != null ? rb.position : transform.position;
    }
}