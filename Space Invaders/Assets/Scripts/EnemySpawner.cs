using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float yPos = 4;
    public float spawnDelay = 2;
    public float spawnRate = 2;
    private float spawnDelayPower = 5;
    private float spawnRatePower = 12;
    private bool canSpawn = true;

    public GameObject[] asteroids;
    public GameObject[] enemies;
    public GameObject[] powerups;
    public GameObject[] asteroidPower;


    private PlayerController playerController;

    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnAllMethod();
    }
    public void SpawnEnemy()
    {
        float spawnPosY = Random.Range(-yPos, yPos);
        Vector2 spawnPos = transform.position;
        spawnPos.y = spawnPosY;

        int chooseEnemy = Random.Range(0, 2);

        if (chooseEnemy == 0)
        {
            Instantiate(asteroids[Random.Range(0, asteroids.Length)], spawnPos, Quaternion.Euler(0, 0, 90));
        }

        else if (chooseEnemy == 1)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        }
    }

    void SpawnPowerUp()
    {
        float spawnPosY = Random.Range(-yPos, yPos);
        Vector2 spawnPos = transform.position;
        spawnPos.y = spawnPosY;

        Instantiate(powerups[Random.Range(0, powerups.Length)], spawnPos, Quaternion.identity);
    }

    void SpawnAsteroidPower()
    {
        float spawnPosY = Random.Range(-yPos, yPos);
        Vector2 spawnPos = transform.position;
        spawnPos.y = spawnPosY;

        Instantiate(asteroidPower[0], spawnPos, Quaternion.identity);
    }

    void SpawnAllMethod()
    {
        while (true)
        {
            if (canSpawn == true)
            {
                Debug.Log("Normal spawn");
                InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
                InvokeRepeating("SpawnPowerUp", spawnDelayPower, spawnRatePower);
            }

            if (playerController.isAsteroid == true)
            {
                canSpawn = false;
                Debug.Log("power spawn");
                for (int i = 0; i < 15; i++)
                {
                    InvokeRepeating("SpawnAsteroidPower", 1, 0.3f);
                }
                canSpawn = true;
            }
        }
    }

    /*
    private IEnumerator CheckBoolEverySecond()
    {
        while (true)
        {
            // Check the bool variable here
            if (playerController.isAsteroid == true)
            {
                canSpawn = false;
                Debug.Log("power spawn");
                for (int i = 0; i < 15; i++)
                {
                    InvokeRepeating("SpawnAsteroidPower", 1, 0.3f);
                }
                canSpawn = true;
            }
            if (playerController.isAsteroid == false)
            {
                Debug.Log("The bool variable is false!");
            }

            // Wait for one second before checking again
            yield return new WaitForSeconds(0.3f);
        }
    }
    */

}
