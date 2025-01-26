using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Vita : MonoBehaviour
{
    public int level;
    public int[] addHealth;

    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.SetMaxHealth(addHealth[level]);

        level++;
        if (level >= addHealth.Length)
        {
            PowerUpManager.Instance.RemovePUCompleted(gameObject);
        }

        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
    }
}
