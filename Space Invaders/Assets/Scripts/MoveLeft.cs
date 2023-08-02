using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 8; 
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);


        if (transform.position.x < -14)
        {
            Destroy(gameObject);
        }
    }
}
