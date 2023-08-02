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

    public GameObject bullet;

    private void Start()
    {
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
            Instantiate(bullet, attackPoint.transform.position, bullet.transform.rotation);
            canShoot = false;
            StartCoroutine(shootDelay());
        }
    }

    IEnumerator shootDelay()
    {
        yield return new WaitForSeconds(1.8f);
        canShoot = true;
    }
}
