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

    public CharacterDatabase characterDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;

    private float yPos = 4;
    private bool canShoot = true;

    private BulletPool bulletPool;
    private GameObject bullet;
    public Queue<GameObject> Bullets = new Queue<GameObject>();

    private void Start()
    {
        bulletPool = GameObject.Find("PoolManager").GetComponent<BulletPool>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            audioManager.playBulletPlayer();
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

    IEnumerator WaitForOver()
    {
        yield return new WaitForSeconds(2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") | collision.CompareTag("EnemyBullet"))
        {
            audioManager.playExpSpaceShip();
            Destroy(gameObject);
            WaitForOver();
            audioManager.playGameOver();
            gameOverScreen.gameOverScreen();
        }
    }
}
