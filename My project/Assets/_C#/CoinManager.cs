using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    [Header("Coin Icons")]
    public GameObject[] coinIcons;

    private int coinCount = 0;
    private const int maxCoins = 2;

    // コイン取得時に呼ぶ
    public void CollectCoin()
    {
        coinCount++;
        if (coinIcons != null && coinCount - 1 < coinIcons.Length && coinCount - 1 >= 0)
        {
            coinIcons[coinCount - 1].SetActive(false);
        }

        if (coinCount == maxCoins)
        {
            SceneManager.LoadScene("GameClear");
        }
    }

    // 必要ならリセット処理も
    public void ResetCoins()
    {
        coinCount = 0;
        if (coinIcons != null)
        {
            foreach (var icon in coinIcons)
            {
                if (icon != null) icon.SetActive(true);
            }
        }
    }
}
