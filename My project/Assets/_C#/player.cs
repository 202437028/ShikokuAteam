using UnityEngine;

public class player : MonoBehaviour
{
    // プレイヤーを y/z 軸のみで移動させる簡易スクリプト
    // - x 軸は固定する
    // - Rigidbody があれば物理移動（FixedUpdate + MovePosition）を使い、
    //   Rigidbody がなければ transform.position を直接操作する

    public float speed = 5f; // 移動速度

    private float initialX; // x 座標を保持
    private Rigidbody rb;

    // 入力キャッシュ（Update で読み取り、FixedUpdate で使用）
    private float inputY = 0f;
    private float inputZ = 0f;

    void Start()
    {
        initialX = transform.position.x;
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // x 軸位置を物理的に固定して回転も固定（必要に応じて調整）
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }
    }

    void Update()
    {
        // Z 軸（前後）はデフォルトの Vertical を使用
        inputZ = Input.GetAxis("Vertical");

        // Y 軸（上下）はキーボードの E (上) / Q (下) を使う（必要なら Input Manager に追加可）
        inputY = 0f;
        if (Input.GetKey(KeyCode.E)) inputY += 1f;
        if (Input.GetKey(KeyCode.Q)) inputY -= 1f;

        // Rigidbody が無ければ transform を直接操作
        if (rb == null)
        {
            Vector3 pos = transform.position;
            pos += new Vector3(0f, inputY * speed * Time.deltaTime, inputZ * speed * Time.deltaTime);
            pos.x = initialX; // x を固定
            transform.position = pos;
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            Vector3 newPos = rb.position + new Vector3(0f, inputY * speed * Time.fixedDeltaTime, inputZ * speed * Time.fixedDeltaTime);
            newPos.x = initialX; // 念のため x を固定
            rb.MovePosition(newPos);
        }
    }
}
