using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float yPos = 4;
    public float spawnDelay = 2;
    public float spawnRate = 2;
    public float spawnDelayPower = 5;
    public float spawnRatePower = 12;
    private float invokecount = 0;
    public bool canSpawn = true;

    public GameObject[] asteroids;
    public GameObject[] enemies;
    public GameObject[] powerups;
    public GameObject[] asteroidPower;


    public PlayerController playerController;

    public void Start()
    {
        StartContinuousSpawn();
        CheckBoolEverySecond();
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

    public void SpawnPowerUp()
    {
        float spawnPosY = Random.Range(-yPos, yPos);
        Vector2 spawnPos = transform.position;
        spawnPos.y = spawnPosY;

        Instantiate(powerups[Random.Range(0, powerups.Length)], spawnPos, Quaternion.identity);
    }

    public void StartMethod()
    {
        InvokeRepeating("StartAsteroidAttack", 0, 0.5f);      
    }

    public void StartAsteroidAttack()
    {
        Debug.Log("Asteroid attack should be started");
        float spawnPosY = Random.Range(-yPos, yPos);
        Vector2 spawnPos = transform.position;
        spawnPos.y = spawnPosY;
        invokecount++;

        Instantiate(asteroidPower[0], spawnPos, Quaternion.identity);

        if (invokecount > 10)
        {
            invokecount = 0;
            StopAsteroidAttack();
            canSpawn = true;
            StartContinuousSpawn();
        }

        //StartCoroutine(AsteroidAttackRate());
    }

    /*
    void SpawnAllMethod()
    {
        if (canSpawn == true)
        {
            Debug.Log("canSpawn = true");
            InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
            InvokeRepeating("SpawnPowerUp", spawnDelayPower, spawnRatePower);
        }

        if (canSpawn == false) 
        {
            Debug.Log("Asteroid Attack should be start");
            StartAsteroidAttack();
        }
    }
    */

    public void StartContinuousSpawn()
    {
        if (canSpawn == true)
        {
            Debug.Log("Continuous Spawn is actived");
            InvokeRepeating("SpawnEnemy", 2, 1.5f);
            InvokeRepeating("SpawnPowerUp", 2, 12);
        }
    }

    public void StopContinuousSpawn()
    {
        Debug.Log("Continuous Spawn is deactived");
        CancelInvoke("SpawnEnemy");
        CancelInvoke("SpawnPowerUp");
    }

    public void StopAsteroidAttack()
    {
        Debug.Log("Asteroid attack ended");
        CancelInvoke("StartAsteroidAttack");
        playerController.isAsteroid = false;
    }

    IEnumerator AsteroidAttackRate()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator CheckBoolEverySecond()
    {
        while (true)
        {
            // Check the bool variable here
            if (canSpawn == true)
            {
                Debug.Log("canSpawn = true");
            }
            if (canSpawn == false)
            {
                Debug.Log("canSpawn = false");
            }

            // Wait for one second before checking again
            yield return new WaitForSeconds(0.5f);
        }
    }

    /*
    void SpawnAllMethod()
    {
        if (canSpawn)
        {
            Debug.Log("SpawnAllMethod is being called.");

            InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
            InvokeRepeating("SpawnPowerUp", spawnDelayPower, spawnRatePower);
        }

        // Check for asteroid condition
        if (playerController.isAsteroid)
        {
           Debug.Log("Asteroid attack is active");
           StopContinuousSpawn();

           InvokeRepeating("SpawnAsteroidPower", 0, 0.5f);

           // Optionally, cancel asteroid-related spawning after a duration
           Invoke("StopAsteroidAttack", 6);            
        }
    }
    */
}
