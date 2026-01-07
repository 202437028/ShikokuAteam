using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    [Header("Respawn Position")]
    public Vector3 respawnPosition = new Vector3(0f, 2f, 0f);


    [Header("Life Points")]
    public GameObject[] LifePoints;

    [Header("残機数設定")]
    [SerializeField]
    private int initialLives = 3;

    [Header("残機数表示用Text")]
    public TextMeshProUGUI livesText;

    [Header("Respawn Count")]
    [SerializeField]
    private int respawnCount = 0;
    private int currentLives;

    private void Awake()
    {
        Instance = this;
        currentLives = initialLives;
        UpdateLivesText();
    }

    public void Respawn(Rigidbody rb)
    {
        if (rb == null) return;

        IncrementRespawn();

        rb.position = respawnPosition;
        rb.linearVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
    }

    public void Respawn(Transform t)
    {
        if (t == null) return;

        IncrementRespawn();

        t.position = respawnPosition;
        t.rotation = Quaternion.identity;
    }

    private void IncrementRespawn()
    {
        respawnCount++;
        currentLives = Mathf.Max(0, initialLives - respawnCount);
        UpdateLifePoints();
        UpdateLivesText();
        // リスポーン時に効果音を鳴らす（ゲームオーバーにならない場合のみ）
        if (AudioManager.Instance != null && currentLives > 0)
        {
            AudioManager.Instance.Play("Respawn");
        }

        if (currentLives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateLifePoints()
    {
        if (LifePoints != null && respawnCount - 1 < LifePoints.Length && respawnCount - 1 >= 0)
        {
            LifePoints[respawnCount - 1].SetActive(false);
        }
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "×" + currentLives.ToString();
        }
    }
}
