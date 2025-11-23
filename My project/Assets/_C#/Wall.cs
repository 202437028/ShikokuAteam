using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Respawn Settings")]
    [Tooltip("Position to move the player to when they touch this wall.")]
    public Vector3 respawnPosition = new Vector3(0f, 10f, 0f);

    [Tooltip("Tag used to identify the player object. If empty, any object will be respawned.")]
    public string playerTag = "Player";

    // Collision handler for non-trigger collider
    private void OnCollisionEnter(Collision collision)
    {
        if (!string.IsNullOrEmpty(playerTag) && !collision.collider.CompareTag(playerTag)) return;

        RespawnObject(collision.collider);
    }

    // Trigger handler for trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(playerTag) && !other.CompareTag(playerTag)) return;

        RespawnObject(other);
    }

    private void RespawnObject(Collider col)
    {
        if (col == null) return;

        // Try to use attached Rigidbody if available to reset position/velocity safely
        var rb = col.attachedRigidbody;
        if (rb != null)
        {
            rb.position = respawnPosition;
            // Reset linear velocity (project uses linearVelocity elsewhere)
            rb.linearVelocity = Vector3.zero;
            // Optionally reset rotation
            rb.rotation = Quaternion.identity;
        }
        else
        {
            // Fallback: move transform directly
            col.transform.position = respawnPosition;
            col.transform.rotation = Quaternion.identity;
        }
    }
}
