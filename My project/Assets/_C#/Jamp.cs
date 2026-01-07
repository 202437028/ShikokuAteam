using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Jamp : MonoBehaviour
{
    [Header("Jump Settings")]
    [Tooltip("Vertical velocity given to the player when stepping on this platform.")]
    public float jumpVelocity = 8f;

    [Tooltip("Additional horizontal boost applied in the player's forward direction (keeps 'inertia').")]
    public float forwardBoost = 2f;

    [Tooltip("If true, this will only affect objects tagged with `playerTag`." )]
    public bool requirePlayerTag = true;
    public string playerTag = "Player";

    [Tooltip("If this collider is configured as a trigger, the trigger handler will be used.")]
    public bool supportTrigger = false;
    [Header("効果音キー (AudioManager)")]
    [Tooltip("AudioManager に登録したキーを指定してください。未設定の場合は AudioManager のデフォルトを使用します。")]
    public string soundKey = "Jamp";

    // Called when a non-trigger collision begins
    private void OnCollisionEnter(Collision collision)
    {
        if (supportTrigger) return; // ignore collisions if configured to use trigger

        if (requirePlayerTag && !collision.collider.CompareTag(playerTag)) return;

        // Ensure contact came from above (player stepped on top)
        bool fromAbove = false;
        foreach (var contact in collision.contacts)
        {
            // contact.normal points from this collider toward the other collider.
            // If normal.y is sufficiently positive, the other object contacted from above.
            if (contact.normal.y > 0.5f)
            {
                fromAbove = true;
                break;
            }
        }

        if (!fromAbove) return;

        var rb = collision.rigidbody ?? collision.collider.attachedRigidbody;
        if (rb == null) return;

        ApplyJump(rb, collision.transform);
    }

    // Called when this collider is a trigger and something enters it
    private void OnTriggerEnter(Collider other)
    {
        if (!supportTrigger) return;

        if (requirePlayerTag && !other.CompareTag(playerTag)) return;

        var rb = other.attachedRigidbody;
        if (rb == null) return;

        // Heuristic: ensure the object is above the platform when entering trigger
        if (!(other.transform.position.y > transform.position.y - 0.5f)) return;

        ApplyJump(rb, other.transform);
    }

    // Apply the jump: set vertical velocity and add a forward (horizontal) boost following player's facing/inertia
    private void ApplyJump(Rigidbody rb, Transform playerTransform)
    {
        // Determine a horizontal forward direction to give the player a slanted jump.
        Vector3 forward = playerTransform.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude < 0.0001f)
        {
            // fallback to current horizontal velocity direction if player isn't facing anywhere
            Vector3 horVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            if (horVel.sqrMagnitude > 0.0001f)
            {
                forward = horVel.normalized;
            }
            else
            {
                forward = Vector3.forward;
            }
        }

        // Preserve existing horizontal velocity and add forward boost
        Vector3 newVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        newVel += forward.normalized * forwardBoost;

        // Set the vertical component to the configured jump velocity
        newVel.y = jumpVelocity;

        // Directly set velocity for an immediate, consistent jump behaviour
        rb.linearVelocity = newVel;

        // 再生: Playerがこのジャンプ台を踏んだら効果音を鳴らす（ジャンプ台固有クリップがあればそれを使う）
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play(soundKey);
        }
    }
}
