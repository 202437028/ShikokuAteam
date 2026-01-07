using UnityEngine;

public class Bone : MonoBehaviour
{
    [Header("効果音キー (AudioManager) ")]
    [Tooltip("AudioManager に登録したキーを指定してください。未設定の場合は AudioManager のデフォルトを使用します。")]
    public string soundKey = "Bone";

    // 他のスクリプトから呼んで収集処理と音再生を行う
    public void Collect()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play(soundKey);
        }
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }
}