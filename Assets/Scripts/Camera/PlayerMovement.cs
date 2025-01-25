using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedRotation;

    private Rigidbody playerRb;


    public Transform firePoint;
    public GameObject projectile;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }
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
}
