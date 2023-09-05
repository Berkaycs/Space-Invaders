using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Camera mainCamera;

    private AudioManager audioManager;
    public GameOverScreen gameOverScreen;
    private EnemySpawner enemySpawner;

    public CharacterDatabase characterDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;

    private float yPos = 4;
    private bool canShoot = true;
    private bool isLimited = false;
    public bool isAsteroid = false;
    public bool isPowerUp = false;

    private BulletPool bulletPool;
    private GameObject bullet;
    public GameObject multishotPrefab;
    public Queue<GameObject> Bullets = new Queue<GameObject>();

    private void Start()
    {
        bulletPool = GameObject.Find("PoolManager").GetComponent<BulletPool>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        Cursor.visible = false;

        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }

        else
        {
            Load();
        }

        UpdateCharacter(selectedOption);

        StartCoroutine(CheckBoolEverySecond());
    }

    void Update()
    {
        Move();
        Shoot();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
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
            if (canShoot == true && isPowerUp == false)
            {
                Debug.Log("You can shoot regularly");
                audioManager.playBulletPlayer();
                bullet = bulletPool.GetPoolObject(0);
                bullet.transform.position = attackPoint.transform.position;
                Bullets.Enqueue(bullet);
                canShoot = false;
                StartCoroutine(shootDelay());
            }

            if (canShoot == true && isPowerUp == true)
            {
                Debug.Log("You can multishot");
                audioManager.playBulletPlayer();
                Instantiate(multishotPrefab, attackPoint.transform.position, multishotPrefab.transform.rotation);               
                canShoot = false;
                StartCoroutine(shootDelay());
                StartCoroutine(PowerUp());
            }
        }
    }
    
    IEnumerator shootDelay()
    {
        if (isLimited == false)
        {
            yield return new WaitForSeconds(0.5f);

            if (isLimited == true)
            {
                canShoot = false;
            }

            else
            {
                canShoot = true;
            }
        }

        if (isLimited == true)
        {
            yield return new WaitForSeconds(5f);
            canShoot = true;
            isLimited = false;
        }   
    }

    private IEnumerator CheckBoolEverySecond()
    {
        while (true)
        {
            // Check the bool variable here
            if (isAsteroid == true)
            {
                Debug.Log("power spawn");
                enemySpawner.StopContinuousSpawn();
            }
            if (isAsteroid == false)
            {
                Debug.Log("normal spawn");
            }

            if (isPowerUp == true)
            {
                Shoot();
            }

            // Wait for one second before checking again
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator PowerUp()
    {
        yield return new WaitForSeconds(10);
        isPowerUp = false;
        Debug.Log("Multishot is deactivated");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") | collision.CompareTag("EnemyBullet"))
        {
            audioManager.playExpSpaceShip();
            Destroy(gameObject);
            audioManager.playGameOver();
            gameOverScreen.gameOverScreen();
        }

        if (collision.CompareTag("Limit"))
        {
            Debug.Log("You can't shoot");
            isLimited = true;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Asteroid"))
        {
            Debug.Log("Here comes asteroid!");
            isAsteroid = true;
            enemySpawner.canSpawn = false;
            Destroy(collision.gameObject);
            enemySpawner.StartMethod();
        }

        if (collision.CompareTag("PowerUp"))
        {
            Debug.Log("You get the multishoot power-up");
            Destroy(collision.gameObject);
            isPowerUp = true;
        }

        if (collision.CompareTag("AsteroidAttack"))
        {
            audioManager.playExpSpaceShip();
            Destroy(gameObject);
            audioManager.playGameOver();
            gameOverScreen.gameOverScreen();
        }
    }
}
