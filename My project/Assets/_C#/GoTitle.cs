using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTitle: MonoBehaviour
{
    public AudioClip sound;

    public void OnStartButtonClicked()
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);

        // １秒後にTitleシーンに遷移する。
        Invoke("GoToTitle", 1.0f);
    }

    void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
