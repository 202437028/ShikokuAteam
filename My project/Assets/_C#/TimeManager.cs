using UnityEngine;
// ★追加
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeLabel;
    public float timeCount;

    void Start()
    {
           timeLabel.text = timeCount.ToString("n1");
    }

    void Update()
    {
        timeCount -= Time.deltaTime;

            // 小数点１位まで表示（数字のみ）
            timeLabel.text = timeCount.ToString("n1");

        if(timeCount < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}