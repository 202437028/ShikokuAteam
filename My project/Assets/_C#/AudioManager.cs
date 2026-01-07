using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("オーディオ設定")]
    [Tooltip("未設定の場合、この GameObject にアタッチされた AudioSource を使用します。")]
    public AudioSource audioSource;

    [Header("サウンド登録")]
    [Tooltip("キーとクリップをセットしておくと、他スクリプトからキー指定で再生できます。")]
    public SoundEntry[] sounds;

    [Header("デフォルトサウンド (フォールバック)")]
    [Tooltip("キーで見つからない場合に再生するクリップ。")]
    public AudioClip defaultClip;

    // 内部マップ
    private System.Collections.Generic.Dictionary<string, AudioClip> soundMap;

    private void Awake()
    {
        Instance = this;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        soundMap = new System.Collections.Generic.Dictionary<string, AudioClip>();
        if (sounds != null)
        {
            foreach (var e in sounds)
            {
                if (e != null && !string.IsNullOrEmpty(e.key) && e.clip != null)
                {
                    soundMap[e.key] = e.clip;
                }
            }
        }
    }

    // Play by registered key. Falls back to defaultClip if key not found.
    public void Play(string key)
    {
        if (audioSource == null) return;
        if (string.IsNullOrEmpty(key))
        {
            if (defaultClip != null) audioSource.PlayOneShot(defaultClip);
            return;
        }

        if (soundMap != null && soundMap.TryGetValue(key, out var clip))
        {
            audioSource.PlayOneShot(clip);
            return;
        }

        if (defaultClip != null)
        {
            audioSource.PlayOneShot(defaultClip);
        }
    }

    // 互換性のために個別クリップ再生も残す
    public void PlaySound(AudioClip c)
    {
        if (audioSource == null || c == null) return;
        audioSource.PlayOneShot(c);
    }
}

[System.Serializable]
public class SoundEntry
{
    public string key;
    public AudioClip clip;
}
