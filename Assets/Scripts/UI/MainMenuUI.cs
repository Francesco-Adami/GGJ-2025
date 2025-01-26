using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour, IGameUI
{
    public UIManager.GameUI type;
    public UIManager.GameUI GetUIType()
    {
        return type;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
