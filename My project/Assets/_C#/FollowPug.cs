using UnityEngine;

public class FollowPug : MonoBehaviour
{
    [Header("Follow Settings")]
    [Tooltip("Playerオブジェクトを指定してください")]
    public Transform playerTransform;

    [Tooltip("PlayerのY座標から見るこのオフセット高さを追従")]
    [SerializeField]
    private float heightOffset = 0f;

    [Tooltip("Playerのz座標から見るこのオフセット距離を追従")]
    [SerializeField]
    private float distanceOffset = 0f;

    void Start()
    {
        // Playerが指定されていなければ、Tagで自動検索
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Playerの位置にオフセットを加えて追従
        Vector3 targetPosition = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y + heightOffset,
            playerTransform.position.z + distanceOffset
        );
        transform.position = targetPosition;
    }
}
