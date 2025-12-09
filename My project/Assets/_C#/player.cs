using UnityEngine;

public class Player : MonoBehaviour
{
    // ★インプットシステム
    private InputSystem_Actions isa;

    // 前方向の一定速度（何も押さずに直進する速さ）
    [SerializeField] public float forwardSpeed = 10f;
    // 左右入力に対する横移動速度
    [SerializeField] public float lateralSpeed = 6f;
    // 左右に移動できる範囲（x座標の最大値）
    [SerializeField] public float maxLateral = 4f;

    // 落下時のリスポーン位置
    [SerializeField] public Vector3 respawnPosition = new Vector3(0f, 2f, 0f);
    // y がこの値以下になったらリスポーン
    [SerializeField] public float fallThresholdY = -10f;

    private Rigidbody rb;

    // "Respawn"タグのオブジェクトに触れたらリスポーン
    void OnTriggerEnter(Collider other)
    {
        // もしもぶつかった相手に「Bone」という「Tag」が付いていたならば（条件）
        if (other.CompareTag("Bone"))
        {
            // ぶつかった相手を破壊する（実行）
            Destroy(other.gameObject);
            // BoneManager に通知してカウントを進める
            if (BoneManager.Instance != null)
            {
                BoneManager.Instance.CollectBone();
            }
        }

        if (other.CompareTag("Respawn"))
        {
            if (RespawnManager.Instance != null)
            {
                RespawnManager.Instance.Respawn(rb);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            if (RespawnManager.Instance != null)
            {
                RespawnManager.Instance.Respawn(rb);
            }
        }
    }

    void Start()
    {
        // ★インプットシステム
        isa = new InputSystem_Actions();
        isa.Enable();

        rb = GetComponent<Rigidbody>();
    }

    // 物理移動は FixedUpdate で行う
    void FixedUpdate()
    {
        // ★インプットシステム
        Vector2 movement2 = isa.Player.Move.ReadValue<Vector2>();

        float inputX = movement2.x; // 左右入力のみ使用

        // 現在の速度を取得
        Vector3 vel = rb.linearVelocity;

        // 常に前方向に一定速度を維持（z軸）
        vel.z = forwardSpeed;

        // 横方向は入力に応じて直接速度を設定
        vel.x = inputX * lateralSpeed;

        // y は物理に任せる（重力など）

        rb.linearVelocity = vel;

        // 位置のxを制限してプレイヤーが左右に行き過ぎないようにする
        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, -maxLateral, maxLateral);
        rb.position = pos;

        // 落下チェック: y が閾値以下になったらリスポーン位置へ移動して速度をリセット
        if (rb.position.y <= fallThresholdY)
        {
              if (RespawnManager.Instance != null)
              {
                 RespawnManager.Instance.Respawn(rb);
              }
        }
    }

    // ★インプットシステム（メモリの解放）
    void OnDisable()
    {
        isa.Disable();
    }
}
