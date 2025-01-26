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
    public List<GameObject> puPrefab;
    private List<GameObject> puToChoose;
    private List<GameObject> puList;
    private const int choosablePowerUp = 3;

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

    #region SET POWERUP
    public void SetNewDamage(int addDamage)
    {
        PlayerManager.Instance.playerDamage += addDamage;
        print("MaxHealth: " + PlayerManager.Instance.playerDamage);
    }

    public void SetMaxHealth(int addHealth)
    {
        PlayerManager.Instance.maxHealth += addHealth;
        print("MaxHealth: " + PlayerManager.Instance.maxHealth);
    }

    public void SetSpeed(int speedPercentage)
    {
        PlayerManager.Instance.speed += PlayerManager.Instance.speed * speedPercentage / 100;
        print("Speed: " + PlayerManager.Instance.speed);
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
        foreach (GameObject gameObject in puPrefab)
        {
            puToChoose.Add(gameObject);
        }

        for (int i = 0; i < choosablePowerUp; i++)
        {
            puList.Add(ChooseOneRandomPowerUp());
        }
    }

    private GameObject ChooseOneRandomPowerUp()
    {
        GameObject a = puToChoose[UnityEngine.Random.Range(0, puToChoose.Count - 1)];
        puToChoose.Remove(a);
        print("CHOOSE: " +  a.name);
        return a;
    }
}
