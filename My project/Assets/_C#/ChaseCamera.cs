using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    // ①変数の定義（データを入れるための箱を作る）
    public GameObject target;
    private Vector3 offset;

    void Start()
    {
        // ②代入（作成した箱の中にデータを入れる）
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        // ③活用（データの入った箱を活用する）
        transform.position = target.transform.position + offset;
    }
}