using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_AddHealth : MonoBehaviour
{
    [Header("PowerUp Info")]
    public int addHealthPercentage;

    [Header("Mesh")]
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private float cooldownRespawn;
    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (!boxCollider.enabled)
        {
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("healing");
            timer = cooldownRespawn;
            boxCollider.enabled = false;
            PlayerManager.Instance.currentHealth += PlayerManager.Instance.maxHealth * addHealthPercentage / 100;
        }
    }
}
