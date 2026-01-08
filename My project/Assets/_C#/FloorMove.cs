using UnityEngine;

public class FallFloor : MonoBehaviour
{
    public float fallDelay = 2.0f; // 落下までの遅延時間

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーが衝突したら、一定時間後に落下を開始
        if (collision.gameObject.CompareTag("Sphere"))
        {
            // 遅延実行（Invokeを使うと簡単）
            Invoke("StartFall", fallDelay);
        }
    }

    void StartFall()
    {
        // Rigidbodyを取得し、isKinematicをfalseにする
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
