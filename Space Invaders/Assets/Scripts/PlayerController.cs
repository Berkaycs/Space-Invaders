using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Camera mainCamera;

    private float yPos = 4;
    private bool canShoot = true;

    public BulletPool bulletPool;
    [SerializeField] private GameObject bullet;
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
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            bullet = bulletPool.GetPoolObject(0);
            bullet.transform.position = attackPoint.transform.position;
            Bullets.Enqueue(bullet);
            canShoot = false;
            StartCoroutine(shootDelay());
        }
    }

    IEnumerator shootDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
