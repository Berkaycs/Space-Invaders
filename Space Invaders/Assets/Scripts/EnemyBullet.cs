using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 15;

    void Update()
    {
        Move();

        if (transform.position.x < -14)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        Vector2 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }
}
