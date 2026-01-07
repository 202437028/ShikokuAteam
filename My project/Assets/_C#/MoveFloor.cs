using UnityEngine;
using System.Collections;
 
public class MoveGround : MonoBehaviour
{
 
    private Vector3 initialPosition;
    public float speed = 1.0f;
    public float distance = 2.0f;
 
    void Start()
    {
 
        initialPosition = transform.position;
 
    }
 
    void Update()
    {
 
        transform.position = new Vector3(initialPosition.x + Mathf.Sin(Time.time * speed) * distance, initialPosition.y , initialPosition.z );
 
    }
 
}