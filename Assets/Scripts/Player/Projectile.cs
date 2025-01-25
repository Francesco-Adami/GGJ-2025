using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 10f;
    private bool isDespawned;
    [SerializeField] private Vector3 despawn;

    private void OnEnable()
    {
        isDespawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDespawned) return;

        transform.position += transform.forward * 10 * Time.deltaTime;
        UpdateDespawnTimer();
    }

    private void UpdateDespawnTimer()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            isDespawned = true;
            transform.position = despawn;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isDespawned = true;
            transform.position = despawn;

            // fa danno
            other.gameObject.GetComponent<Enemy>().TakeDamage(PlayerManager.Instance.GetPlayerDamage());
        }
    }
}