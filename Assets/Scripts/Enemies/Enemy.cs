using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Base Stats")]
    public int level;
    public int enemyHealth;
    public float enemySpeed;

    [Header("Enemy Attack Stats")]
    public int enemyDamage;
    public int fireRate;
    public int attackRange;
    private bool isPlayerInRange;

    [Header("LevelUp Stats")]
    [SerializeField] private int addHealth;
    [SerializeField] private int addDamage;
    [SerializeField] private float addSpeed;

    [Header("Enemy Movement")]
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] private SphereCollider attackRangeCollider;

    // Enemy Target
    public Vector3 playerPosition;
    // private playerClass player;


    // UNITY FUNCTIONS
    private void Start()
    {
        attackRangeCollider.radius = attackRange;
        SetDestinationToPlayer();
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            StartCoroutine(AttackPlayer());
        }
        else
        {
            MoveToPlayer();
        }
    }

    // POOLING OF THE ENEMY
    public void SpawnEnemy(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        gameObject.SetActive(true);
        SetDestinationToPlayer();
    }

    #region STATS
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        CheckIfIsDead();
    }

    private void CheckIfIsDead()
    {
        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public int GetEnemyHealth() { return enemyHealth; }
    #endregion

    #region MOVEMENT
    public void SetDestinationToPlayer()
    {
        playerPosition = FindAnyObjectByType<PlayerTest>().transform.position;
    }

    private void MoveToPlayer()
    {
        enemyAgent.SetDestination(playerPosition);
    }

    #endregion

    #region ATTACK
    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(fireRate);

        //player.DamagePlayer(enemyDamage);
    }
    #endregion


    // TRIGGER ENTER / EXIT
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
