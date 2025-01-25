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
    private int realHealth;
    public float enemySpeed;
    private float realSpeed;

    [Header("Enemy Attack Stats")]
    public int enemyDamage;
    private int realDamage;
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
    private PlayerManager player;


    // UNITY FUNCTIONS
    private void OnEnable()
    {
        attackRangeCollider.radius = attackRange;
        SetPlayerStats();

        SetDestinationToPlayer();
    }

    private void SetPlayerStats()
    {
        realHealth = enemyHealth + addHealth * (level - 1);
        realSpeed = enemySpeed + addSpeed * (level - 1);
        realDamage = enemyDamage + addDamage * (level - 1);

        enemyAgent.speed = realSpeed;
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
        realHealth -= damage;
        CheckIfIsDead();
    }

    private void CheckIfIsDead()
    {
        if (realHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region MOVEMENT
    public void SetDestinationToPlayer()
    {
        player = FindAnyObjectByType<PlayerManager>();
        playerPosition = player.transform.position;
    }

    private void MoveToPlayer()
    {
        enemyAgent.SetDestination(playerPosition);
    }

    #endregion

    #region ATTACK
    private IEnumerator AttackPlayer()
    {
        // TODO OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        // start animation
        yield return new WaitForSeconds(fireRate);

        player.DamagePlayer(enemyDamage);
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
