using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    #region SINGLETON
    private static EnemiesManager instance;
    public static EnemiesManager Instance
    {
        get { return instance; }
    }
    
    #endregion

    public List<Enemy> enemiesPool1;
    public List<Enemy> enemiesPool2;
    public List<Enemy> enemiesPool3;

    public List<Transform> spawnPoints;

    public int currentWave;
    [SerializeField] private int levelUpEnemiesEveryXWaves;
    [HideInInspector] public int enemiesSpawned = 0;

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

        ResetAllEnemies();
    }

    private void Start()
    {

    }

    private void ResetAllEnemies()
    {
        foreach (Enemy enemy in enemiesPool1)
        {
            enemy.gameObject.SetActive(false);
        }

        foreach (Enemy enemy in enemiesPool2)
        {
            enemy.gameObject.SetActive(false);
        }

        foreach (Enemy enemy in enemiesPool3)
        {
            enemy.gameObject.SetActive(false);
        }
    }


    #region WAVES
    public void StartNextWave()
    {
        // Reset
        ResetAllEnemies();
        enemiesSpawned = 0;

        // Set New Wave stats
        currentWave++;
        LevelUpEnemies();

        // Spawn
        SpawnAtLeastOneOfEachEnemy();
        while (!AllEnemiesAreSpawned())
        {
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        int random = UnityEngine.Random.Range(0, 2);
        Enemy enemy;

        switch (random)
        {
            case 0:
                if (enemiesPool1.Count > 0 && GetFirstAvailableEnemy(enemiesPool1))
                {
                    enemy = GetFirstAvailableEnemy(enemiesPool1);
                    enemy.SpawnEnemy(GetRandomSpawnPoint());
                }
                break;
            case 1:
                if (enemiesPool2.Count > 0 && GetFirstAvailableEnemy(enemiesPool2))
                {
                    enemy = GetFirstAvailableEnemy(enemiesPool2);
                    enemy.SpawnEnemy(GetRandomSpawnPoint());
                }
                break;
            case 2:
                if (enemiesPool3.Count > 0 && GetFirstAvailableEnemy(enemiesPool3))
                {
                    enemy = GetFirstAvailableEnemy(enemiesPool3);
                    enemy.SpawnEnemy(GetRandomSpawnPoint());
                }
                break;
        }
        enemiesSpawned++;
    }

    private void SpawnAtLeastOneOfEachEnemy()
    {
        if (currentWave >= 3)
        {
            if (enemiesPool1.Count > 0 && GetFirstAvailableEnemy(enemiesPool1))
            {
                GetFirstAvailableEnemy(enemiesPool1).SpawnEnemy(GetRandomSpawnPoint());
                enemiesSpawned++;
            }
            if (enemiesPool2.Count > 0 && GetFirstAvailableEnemy(enemiesPool2))
            {
                GetFirstAvailableEnemy(enemiesPool2).SpawnEnemy(GetRandomSpawnPoint());
                enemiesSpawned++;
            }
            if (enemiesPool3.Count > 0 && GetFirstAvailableEnemy(enemiesPool3))
            {
                GetFirstAvailableEnemy(enemiesPool3).SpawnEnemy(GetRandomSpawnPoint());
                enemiesSpawned++;
            }
        }
    }

    private bool AllEnemiesAreSpawned()
    {
        if (enemiesSpawned >= currentWave) return true;
        return false;
    }

    private void LevelUpEnemies()
    {
        int lvlUp = currentWave / levelUpEnemiesEveryXWaves;

        print("lvlUP: " + lvlUp);

        foreach (Enemy enemy in enemiesPool1)
        {
            enemy.level = lvlUp + 1;
        }

        foreach (Enemy enemy in enemiesPool2)
        {
            enemy.level = lvlUp + 1;
        }

        foreach (Enemy enemy in enemiesPool3)
        {
            enemy.level = lvlUp + 1;
        }
    }

    #endregion

    public Enemy GetFirstAvailableEnemy(List<Enemy> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    public bool IsWaveFinished()
    {
        if (enemiesSpawned <= 0) return true;
        return false;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int index = UnityEngine.Random.Range(0, spawnPoints.Count - 1);
        return spawnPoints[index].position;
    }
}
