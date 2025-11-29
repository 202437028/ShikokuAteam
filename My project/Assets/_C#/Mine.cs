using UnityEngine;

public class Mine : MonoBehaviour
{
    [Header("Respawn Settings")]
    [Tooltip("Position to move the player to when they hit this mine. Default is (0,11,0).")]
    public Vector3 respawnPosition = new Vector3(0f, 2f, 0f);

    [Tooltip("Tag used to identify the player object. Leave empty to affect any object.")]
    public string playerTag = "Player";

    // Called when a non-trigger collision begins
    private void OnCollisionEnter(Collision collision)
    {
        if (!string.IsNullOrEmpty(playerTag) && !collision.collider.CompareTag(playerTag)) return;

        RespawnCollider(collision.collider);
    }

    // Called when this collider is a trigger and something enters it
    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(playerTag) && !other.CompareTag(playerTag)) return;

        RespawnCollider(other);
    }

    private void RespawnCollider(Collider col)
    {
        if (col == null) return;

        var rb = col.attachedRigidbody;
        if (rb != null)
        {
            rb.position = respawnPosition;
            rb.linearVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
        }
        else
        {
            col.transform.position = respawnPosition;
            col.transform.rotation = Quaternion.identity;
        }
    }
}
