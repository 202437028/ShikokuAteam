using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BoneManager : MonoBehaviour
{
    public static BoneManager Instance;

    [Header("Bone Icons")]
    public GameObject[] boneIcons;

    [Header("Bone Count Display")]
    [Tooltip("総ボーン数。0のままだと boneIcons.Length を使用します。")]
    public int totalBones = 0;

    [Tooltip("ボーンカウント表示用 TextMeshProUGUI をアタッチしてください。")]
    public TextMeshProUGUI boneCountText;

    private int boneCount = 0;
    private int maxBones = 2;

    private void Awake()
    {
        Instance = this;

        // 総数が設定されていなければアイコン配列の長さを使う
        if (totalBones <= 0 && boneIcons != null)
        {
            totalBones = boneIcons.Length;
        }
        maxBones = totalBones > 0 ? totalBones : maxBones;

        // 初期表示を 0/n にセット
        UpdateBoneText();
    }

    // Bone取得時に呼ぶ
    public void CollectBone()
    {
        boneCount++;
        if (boneIcons != null && boneCount - 1 < boneIcons.Length && boneCount - 1 >= 0)
        {
            boneIcons[boneCount - 1].SetActive(false);
        }

        UpdateBoneText();

        if (boneCount >= maxBones)
        {
            SceneManager.LoadScene("GameClear");
        }
    }

    // 必要ならリセット処理も
    public void ResetBones()
    {
        boneCount = 0;
        if (boneIcons != null)
        {
            foreach (var icon in boneIcons)
            {
                if (icon != null) icon.SetActive(true);
            }
        }
        UpdateBoneText();
    }

    private void UpdateBoneText()
    {
        if (boneCountText != null)
        {
            int dispTotal = totalBones > 0 ? totalBones : 0;
            boneCountText.text = boneCount.ToString() + "/" + dispTotal.ToString();
        }
    }
}
