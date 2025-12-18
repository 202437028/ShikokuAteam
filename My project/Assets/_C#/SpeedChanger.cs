using UnityEngine;
using System.Collections.Generic;

public class SpeedChanger : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Tag of the object to affect (usually \"Player\").")]
    public string targetTag = "Player";

    [Header("Speed Changes")]
    public bool changeForward = true;
    [Tooltip("Amount to add to player's forwardSpeed while in contact (can be negative).")]
    public float forwardDelta = 5f;

    public bool changeLateral = false;
    [Tooltip("Amount to add to player's lateralSpeed while in contact (can be negative).")]
    public float lateralDelta = 0f;

    [Header("Behavior")]
    [Tooltip("If true use trigger events; otherwise use collision events.")]
    public bool useTrigger = true;

    // track which Player instances we've modified so we can revert correctly
    private HashSet<Player> modifiedPlayers = new HashSet<Player>();

    void Apply(Player p)
    {
        if (p == null) return;
        if (modifiedPlayers.Contains(p)) return;

        if (changeForward) p.forwardSpeed += forwardDelta;
        if (changeLateral) p.lateralSpeed += lateralDelta;

        modifiedPlayers.Add(p);
    }

    void Revert(Player p)
    {
        if (p == null) return;
        if (!modifiedPlayers.Contains(p)) return;

        if (changeForward) p.forwardSpeed -= forwardDelta;
        if (changeLateral) p.lateralSpeed -= lateralDelta;

        modifiedPlayers.Remove(p);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!useTrigger) return;
        if (!other.CompareTag(targetTag)) return;
        var p = other.GetComponent<Player>();
        Apply(p);
    }

    void OnTriggerExit(Collider other)
    {
        if (!useTrigger) return;
        if (!other.CompareTag(targetTag)) return;
        var p = other.GetComponent<Player>();
        Revert(p);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (useTrigger) return;
        if (!collision.gameObject.CompareTag(targetTag)) return;
        var p = collision.gameObject.GetComponent<Player>();
        Apply(p);
    }

    void OnCollisionExit(Collision collision)
    {
        if (useTrigger) return;
        if (!collision.gameObject.CompareTag(targetTag)) return;
        var p = collision.gameObject.GetComponent<Player>();
        Revert(p);
    }

    void OnDisable()
    {
        // revert any remaining modifications to avoid leaving player in modified state
        foreach (var p in new List<Player>(modifiedPlayers))
        {
            Revert(p);
        }
    }
}
