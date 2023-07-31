using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool canShoot = true;
    public Transform attackPoint;
    [SerializeField] private ParticleSystem explosionParticle;
    private AudioManager audioManager;

    private GameObject bullet;
    public BulletPool bulletPool;

    public Queue<GameObject> Bullets = new Queue<GameObject>();

    private void Start()
    {
        bulletPool = GameObject.Find("PoolManager").GetComponent<BulletPool>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        DestroyOutOfBounds();
        Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") | collision.CompareTag("Bullet") | collision.CompareTag("Enemy"))
        {
            audioManager.playExpSpaceShip();
            explosionParticle.Play();
            Destroy(gameObject);
        }
    }
    void DestroyOutOfBounds()
    {
        if (gameObject.transform.position.x < -14)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (canShoot == true)
        {
            audioManager.playBulletEnemy();
            bullet = bulletPool.GetPoolObject(1);
            bullet.transform.position = attackPoint.transform.position;
            Bullets.Enqueue(bullet);
            canShoot = false;
            StartCoroutine(shootDelay());
        }
    }

    IEnumerator shootDelay()
    {
        yield return new WaitForSeconds(1.2f);
        canShoot = true;
    }
}
