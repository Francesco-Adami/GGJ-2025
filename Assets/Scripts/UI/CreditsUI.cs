using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour, IGameUI
{
    public UIManager.GameUI gameUI;
    public UIManager.GameUI GetUIType()
    {
        return gameUI;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void GoBackToMain()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Main);
    }
}
