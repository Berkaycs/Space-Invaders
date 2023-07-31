using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 15;
    public EnemyController enemyController;

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }
    /*
    void Reuse()
    {
        if (gameObject.transform.position.x < -14)
        {
            if (enemyController.Bullets.Count == 0) return;
            enemyController.Bullets.Dequeue();
            enemyController.bulletPool.SetPoolObject(gameObject, 1);
        }
    }
    */
}
