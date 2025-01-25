using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region SINGLETON
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get { return instance; }
    }

    #endregion

    [Header("Player info")]
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    [Header("Player settings")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    private Rigidbody playerRb;
    private float fireCooldown = 0f;
    [SerializeField] private float fireRate = 1f;

    [Header("Player Attack")]
    public Transform firePoint;
    //public GameObject projectilePrefab;
    public int playerDamage;

    [Header("Projectile pool")]
    [SerializeField] private float reloadTime;
    public List<Projectile> projectiles;
    private bool isReloading;


    private bool enableShoot = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        ReloadBullets();
    }

    private void FixedUpdate()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        Move();
        Cooldown();
        Shoot();

        if (GetFirstBulletAvailable() == null && !isReloading)
        {
            print("Reloading");
            StartCoroutine(ReloadRoutine());
        }
    }

    private void Move()
    {
        // movement = ((transform.forward * Input.GetAxis("Vertical")) + transform.right * Input.GetAxis("Horizontal")) * speed;
        Vector3 movement;
        movement = transform.forward * Input.GetAxis("Vertical") * speed;
        playerRb.AddForce(movement, ForceMode.Force);
        movement = transform.right * Input.GetAxis("Horizontal") * speed;
        playerRb.AddForce(movement, ForceMode.Force);
    }

    #region SHOOT
    protected void Cooldown()
    {
        if (enableShoot) return;
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            enableShoot = true;
        }
    }
    
    private void Shoot() 
    {
        if (Input.GetMouseButton(0))
        {
            if (enableShoot)
            {
                SpawnBullet(firePoint.position, firePoint.rotation);
                // Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                fireCooldown = 1f / fireRate;
                enableShoot = false;
            }
        }
    }

    public Projectile GetFirstBulletAvailable()
    {
        foreach (Projectile p in projectiles)
        {
            if (!p.gameObject.activeInHierarchy) return p;
        }
        return null;
    }

    public void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        Projectile p = GetFirstBulletAvailable();
        if (p == null) return;

        p.gameObject.transform.position = position;
        p.gameObject.transform.rotation = rotation;
        p.gameObject.SetActive(true);
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        ReloadBullets();
        isReloading = false;
    }

    public void ReloadBullets()
    {
        foreach (Projectile p in projectiles)
        {
            p.gameObject.SetActive(false);
        }
    }
    #endregion

    public int GetPlayerDamage()
    {
        return playerDamage + PowerUpManager.Instance.powerUpDanno.addDamage;
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            print("Sei morto");
        }
    }
}
