using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float yPos = 4;
    private float spawnDelay = 2;
    private float spawnRate = 2;
    private float rotationSpeed = 20;

    public GameObject[] asteroids;
    public GameObject[] enemies;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
    }

    void SpawnEnemy()
    {
        float spawnPosY = Random.Range(-yPos, yPos);
        Vector2 spawnPos = transform.position;
        spawnPos.y = spawnPosY;

        int chooseEnemy = Random.Range(0, 2);

        if (chooseEnemy == 0)
        {
            Instantiate(asteroids[Random.Range(0, asteroids.Length)], spawnPos, Quaternion.Euler(0,0,90));
        }
        
        else if (chooseEnemy == 1)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        }
    }
}
