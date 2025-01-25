using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    private Rigidbody playerRb;
    private float fireCooldown = 0f;
    [SerializeField] private float fireRate = 1f;

    public Transform firePoint;
    public GameObject projectilePrefab;

    private bool enableShoot = false;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Cooldown();
        Shoot();
    }

    private void Move()
    {
        /*Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerRb.velocity = movement * speed;
        if (playerRb.velocity.magnitude > maxSpeed) { playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed); }*/

        Vector3 movement = transform.forward * Input.GetAxis("Vertical") * speed;
        playerRb.AddForce(movement, ForceMode.Force);
        movement = transform.right * Input.GetAxis("Horizontal") * speed;
        playerRb.AddForce(movement, ForceMode.Force);
    }

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
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                fireCooldown = 1f / fireRate;
                enableShoot = false;
            }
        }
    }    
}
