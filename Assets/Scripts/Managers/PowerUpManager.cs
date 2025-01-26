using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    #region SINGLETON
    private static PowerUpManager instance;
    public static PowerUpManager Instance
    {
        get { return instance; }
    }

    #endregion

    [Header("UI PowerUp Prefab")]
    public List<GameObject> puPrefab = new();
    private List<GameObject> puToChoose = new();
    public List<GameObject> puList = new();
    private const int choosablePowerUp = 3;

    public List<GameObject> backupList = new();

    [Header("Backup variables")]
    public int damage;
    public int health;
    public float speed;
    public float fireRate;

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
    }

    private void Start()
    {
        damage = PlayerManager.Instance.playerDamage;
        health = PlayerManager.Instance.maxHealth;
        speed = PlayerManager.Instance.speed;
        fireRate = PlayerManager.Instance.fireRate;
    }

    #region SET POWERUP
    public void SetNewDamage(int addDamage)
    {
        PlayerManager.Instance.playerDamage += addDamage;
    }

    public void SetMaxHealth(int addHealth)
    {
        PlayerManager.Instance.maxHealth += addHealth;
        print("MaxHealth: " + PlayerManager.Instance.maxHealth);
    }

    public void SetSpeed(int speedPercentage)
    {
        PlayerManager.Instance.speed += PlayerManager.Instance.speed * speedPercentage / 100;
    }

    public void SetDash()
    {
        PlayerManager.Instance.canDash = true;
    }

    public void SetFireRate(float fireRate)
    {
        PlayerManager.Instance.fireRate += PlayerManager.Instance.fireRate * fireRate / 100;
    }
    #endregion

    public void RemovePUCompleted(GameObject gameObjectToRemove)
    {
        puPrefab.Remove(gameObjectToRemove);
    }

    public void Get3RandomPowerUps()
    {
        puList.Clear();
        foreach (GameObject gameObject in puPrefab)
        {
            puToChoose.Add(gameObject);
        }

        for (int i = 0; i < choosablePowerUp; i++)
        {
            GameObject tmp = ChooseOneRandomPowerUp();
            if (tmp != null)
                puList.Add(tmp);
        }
    }

    private GameObject ChooseOneRandomPowerUp()
    {
        GameObject a = puToChoose[UnityEngine.Random.Range(0, puToChoose.Count - 1)];
        puToChoose.Remove(a);
        return a;
    }

    public void ResetPowerUp()
    {
        puPrefab.Clear();
        puPrefab.AddRange(backupList);

        PlayerManager.Instance.playerDamage = damage;
        PlayerManager.Instance.maxHealth = health;
        PlayerManager.Instance.speed = speed;
        PlayerManager.Instance.fireRate = fireRate;
    }
}
