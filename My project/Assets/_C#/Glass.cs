using UnityEngine;

public class Glass : MonoBehaviour
{
    [Header("Destroy Settings")]
    [Tooltip("Delay in seconds before the object is destroyed after the Player touches it.")]
    [SerializeField]
    private float destroyDelay = 0.5f;

    // Ensure inspector values are valid
    private void OnValidate()
    {
        if (destroyDelay < 0f) destroyDelay = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Playerがこのオブジェクトに接触したときに呼ばれる
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    // Colliderを使う場合
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}