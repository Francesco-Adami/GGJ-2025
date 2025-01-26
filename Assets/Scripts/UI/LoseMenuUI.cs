using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenuUI : MonoBehaviour, IGameUI
{
    public AudioClip mainClip;

    [SerializeField] private UIManager.GameUI gameUI;
    public UIManager.GameUI GetUIType()
    {
        return gameUI;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void BackToMain()
    {
        SoundManager.Instance.PlayMusic(mainClip);
        UIManager.Instance.ShowUI(UIManager.GameUI.Main);
    }
}
