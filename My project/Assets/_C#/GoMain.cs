using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public AudioClip sound;

    public void OnStartButtonClicked()
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);

        // １秒後にMainシーンに遷移する。
        Invoke("GoToMain", 1.0f);
    }

    void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }
}