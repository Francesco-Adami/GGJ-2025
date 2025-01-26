using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Danno : MonoBehaviour
{
    public int level;
    public int[] addDamage;

    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.SetNewDamage(addDamage[level]);
        level++;
        if (level >= addDamage.Length)
        {
            PowerUpManager.Instance.RemovePUCompleted(gameObject);
        }
        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
        GameManager.Instance.isGameStarted = true;
        EnemiesManager.Instance.StartNextWave();
    }
}
