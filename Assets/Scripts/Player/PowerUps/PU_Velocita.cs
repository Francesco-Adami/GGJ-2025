using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Velocita : MonoBehaviour
{
    public int level;
    public int[] addSpeed;

    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.SetSpeed(addSpeed[level]);
        level++;
        if (level >= addSpeed.Length)
        {
            PowerUpManager.Instance.RemovePUCompleted(gameObject);
        }
        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
    }
}
