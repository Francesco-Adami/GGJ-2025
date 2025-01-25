using System;
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
    public float speed;
    //[SerializeField] private float maxSpeed;

    private Rigidbody playerRb;
    private float fireCooldown = 0f;

    [Header("Player Attack")]
    [SerializeField] private float fireRate = 1f;
    public Transform firePoint;
    public int playerDamage;

    [Header("Projectile pool")]
    [SerializeField] private float reloadTime;
    public List<Projectile> projectiles;
    private bool isReloading;

    Vector3 movement;
    private bool enableShoot = false;

    [Header("Player PowerUps")]
    public bool canDash = false;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashForce;

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

    private void Update()
    {
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift)) && canDash)
        {
            Dash();
        }
    }

    private void FixedUpdate()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Move();
        Cooldown();
        Shoot();

        if (GetFirstBulletAvailable() == null && !isReloading)
        {
            print("Reloading");
            StartCoroutine(ReloadRoutine());
        }
    }

    #region DASH
    private void Dash()
    {
        if (movement.magnitude == 0)
            movement = Vector3.forward * speed;    
        playerRb.AddForce(movement * dashForce, ForceMode.Impulse);
        canDash = false;
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
    #endregion

    private void Move()
    {
        movement = ((transform.forward * Input.GetAxis("Vertical")) + transform.right * Input.GetAxis("Horizontal")) * speed;
        playerRb.AddForce(movement, ForceMode.VelocityChange);
        
        //movement = transform.forward * Input.GetAxis("Vertical") * speed;
        //movement = transform.right * Input.GetAxis("Horizontal") * speed;
        //playerRb.AddForce(movement, ForceMode.Force);
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
                //SpawnBullet(firePoint.position, firePoint.rotation);
                float a;
                if (PowerUpManager.Instance.powerUpFireRate == null) a = 0;
                else a = PowerUpManager.Instance.powerUpFireRate.fireRate;
                fireCooldown = 1f / (fireRate + (fireRate * a / 100));
                print("FireRate: " + (fireRate + (fireRate * a / 100)));
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
        return playerDamage;
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
