using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 100;

    private BulletPool bulletPool;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        bulletPool = GameObject.Find("PoolManager").GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Reuse();
    }

    void Move()
    {
        Vector2 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }

    void Reuse()
    {
        if (transform.position.x > 14)
        {
            if (playerController.Bullets.Count == 0) return;
            playerController.Bullets.Dequeue();
            bulletPool.SetPoolObject(gameObject, 0);
        }
    }
}
