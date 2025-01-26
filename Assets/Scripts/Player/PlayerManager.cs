using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    public float fireRate = 1f;
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

    [Header("Player Camera")]
    public float mouseSensitivity = 100f;
    public float verticalRotationLimit = 80f;
    //public Transform point;
    //public Transform head;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineComposer composer;

    [Header("Animator")]
    public Animator animator;
    //public Animator animator2;

    [Header("Audio")]
    public AudioClip attackClip;
    public AudioClip LoseClip;


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
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        }

        

        currentHealth = maxHealth;
        ReloadBullets();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted) return;
        else Cursor.lockState = CursorLockMode.Locked;

        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift)) && canDash)
        {
            Dash();
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Move();
        RotateWithMouse();
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

    #region MOVEMENT
    private void Move()
    {
        movement = ((transform.forward * Input.GetAxis("Vertical")) + transform.right * Input.GetAxis("Horizontal")) * speed * Time.deltaTime;
        playerRb.AddForce(movement, ForceMode.VelocityChange);
        if (movement.magnitude > 0)
        {
            animator.SetBool("velocity", true);
            //animator2.SetBool("velocity", true);
        }
        else
        {
            animator.SetBool("velocity", false);
            //animator2.SetBool("velocity", false);
        }
    }

    private void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = -(Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);

        // Ruota il giocatore sull'asse Y (orizzontale)
        transform.Rotate(Vector3.up * mouseX * 20);

        if (composer != null)
        {
            // Modifica l'offset verticale del Cinemachine Composer
            float newVerticalOffset = composer.m_ScreenY - mouseY;
            //composer.m_ScreenY = Mathf.Clamp(newVerticalOffset, 0.5f - verticalRotationLimit / 90f, 0.5f + verticalRotationLimit / 60f);
            composer.m_ScreenY = Mathf.Clamp(newVerticalOffset, 0.5f - verticalRotationLimit / 240f, 0.5f + verticalRotationLimit / 50f);
        }
    }

    #endregion

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
                GetComponent<AudioSource>().clip = attackClip;
                GetComponent<AudioSource>().Play();
                SpawnBullet(firePoint.position, firePoint.rotation);
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

    public int GetAvailableBullets()
    {
        int i = 0;
        foreach (Projectile p in projectiles)
        {
            if (!p.gameObject.activeInHierarchy) i++;
        }
        return i;
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
            SoundManager.Instance.PlayMusic(LoseClip);
            GameManager.Instance.ResetAll();
            GameManager.Instance.PlayerDead();
        }
    }
}
