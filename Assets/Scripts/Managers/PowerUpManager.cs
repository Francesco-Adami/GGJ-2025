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

    public PU_FireRate powerUpFireRate;

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

    public void SetDash()
    {
        PlayerManager.Instance.canDash = true;
    }

    public void SetSpeed(int speedPercentage)
    {
        PlayerManager.Instance.speed += PlayerManager.Instance.speed * speedPercentage / 100;
        print("Speed: " + PlayerManager.Instance.speed);
    }

}
