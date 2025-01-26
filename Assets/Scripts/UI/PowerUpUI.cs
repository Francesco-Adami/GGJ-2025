using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUI : MonoBehaviour, IGameUI
{
    
    public Transform horizontalLayout;

    public UIManager.GameUI gameUI;
    public UIManager.GameUI GetUIType()
    {
        return gameUI;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnEnable()
    {
        PowerUpManager.Instance.Get3RandomPowerUps();

        foreach (Transform child in horizontalLayout)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            Instantiate(PowerUpManager.Instance.puList[i], horizontalLayout);
        }
    }

    public void StartNextWave()
    {
        GameManager.Instance.isGameStarted = true;
        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
    }
}
