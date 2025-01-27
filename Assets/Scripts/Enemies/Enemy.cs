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
    private bool canAttack = true;

    [Header("Audio")]
    public AudioClip attackSound;


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
        if (!GameManager.Instance.isGameStarted) return;

        if (isPlayerInRange && canAttack)
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
        player = FindAnyObjectByType<PlayerManager>();
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
            EnemiesManager.Instance.enemiesSpawned--;
            if (EnemiesManager.Instance.IsWaveFinished())
            {
                GameManager.Instance.WaveFinished();
            }
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region MOVEMENT
    public void SetDestinationToPlayer()
    {
        if (player == null) return;
        playerPosition = player.transform.position;
    }

    private void MoveToPlayer()
    {
        SetDestinationToPlayer();
        enemyAgent.SetDestination(playerPosition);
    }

    #endregion

    #region ATTACK
    private IEnumerator AttackPlayer()
    {
        // TODO OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        // start animation
        canAttack = false;
        yield return new WaitForSeconds(fireRate);

        player.DamagePlayer(enemyDamage);
        GetComponent<AudioSource>().clip = attackSound;
        GetComponent<AudioSource>().Play();
        canAttack = true;
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
