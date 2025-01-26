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

        for (int i = 0; i < 3; i++)
        {
            Instantiate(PowerUpManager.Instance.puList[i], horizontalLayout);
        }
    }
}
