using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletPool bulletPool;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Camera mainCamera;

    private float yPos = 4;

    public Queue<GameObject> Bullets = new Queue<GameObject>();

    private void Start()
    {
        Cursor.visible = false;   
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.x = -12;

        if (mouseWorldPosition.y < -yPos)
        {
            mouseWorldPosition.y = -yPos;
        }

        else if (mouseWorldPosition.y > yPos)
        {
            mouseWorldPosition.y = yPos;
        }

        transform.position = mouseWorldPosition;
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var Bullet = bulletPool.GetPoolObject(0);
            Bullet.transform.position = attackPoint.transform.position;
            Bullets.Enqueue(Bullet);
        }
    }
}
