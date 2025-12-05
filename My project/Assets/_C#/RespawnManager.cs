using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    [Header("Respawn Position")]
    public Vector3 respawnPosition = new Vector3(0f, 2f, 0f);

    private void Awake()
    {
        Instance = this;
    }

    public void Respawn(Rigidbody rb)
    {
        if (rb == null) return;

        rb.position = respawnPosition;
        rb.linearVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
    }

    public void Respawn(Transform t)
    {
        if (t == null) return;

        t.position = respawnPosition;
        t.rotation = Quaternion.identity;
    }
}
