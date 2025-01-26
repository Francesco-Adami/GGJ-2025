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

    public void GoToPowerUp()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.PowerUps);
    }

    public void GoToCredits()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Credits);
    }

    public void StartGame()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
        GameManager.Instance.isGameStarted = true;
        EnemiesManager.Instance.StartNextWave();
    }


}
