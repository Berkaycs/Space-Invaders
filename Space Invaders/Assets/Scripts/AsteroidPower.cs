using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem explosionParticle;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, 2);
        DestroyOutOfBounds();
    }

    void DestroyOutOfBounds()
    {
        if (gameObject.transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") | collision.CompareTag("Bullet"))
        {
            audioManager.playExpAsteroid();
            explosionParticle.Play();
            Destroy(gameObject);
        }
    }
}
