using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PU_Dash : MonoBehaviour
{
    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.SetDash();

        PowerUpManager.Instance.RemovePUCompleted(gameObject);
        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
        GameManager.Instance.isGameStarted = true;
        EnemiesManager.Instance.StartNextWave();
    }
}
