using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WarpFloor : MonoBehaviour
{
    [Tooltip("移動先の Transform を指定します（必須）")]
    public Transform warpTarget;

    [Tooltip("ワープ対象のタグ。空欄の場合はタグチェックを行いません。")]
    public string requiredTag = "Player";

    [Tooltip("ワープ時に回転も合わせるかどうか")]
    public bool matchRotation = false;

    [Tooltip("ワープ後に Rigidbody の速度をリセットするか")]
    public bool resetRigidbodyVelocity = true;

    void Awake()
    {
        var col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (warpTarget == null) return;
        if (!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag)) return;

        // ワープ対象のルート（Rigidbody がアタッチされている場合はそのオブジェクト）を移動する
        Transform targetRoot = other.transform;
        if (other.attachedRigidbody != null)
        {
            targetRoot = other.attachedRigidbody.transform;
        }

        // 位置を瞬間移動
        targetRoot.position = warpTarget.position;

        if (matchRotation)
        {
            targetRoot.rotation = warpTarget.rotation;
        }

        // Rigidbody があるなら速度をリセット
        var rb = targetRoot.GetComponent<Rigidbody>();
        if (rb != null && resetRigidbodyVelocity)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // CharacterController がある場合は補正のために小さく Move
        var cc = targetRoot.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.Move(Vector3.zero);
        }
    }
}
