using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region SINGLETON
    private static UIManager Instance;
    public static UIManager instance
    {
        get
        {
            if (Instance == null)
                Instance = FindAnyObjectByType<UIManager>();
            return Instance;
        }
    }
    #endregion


    public enum GameUI
    {
        NONE,
        MainMenu,
        InGame,
    }

    private Dictionary<GameUI, IGameUI> registeredUIs = new Dictionary<GameUI, IGameUI>();
    public Transform UIContainer;
    private GameUI currentActiveUI = GameUI.NONE;
    public GameUI startingGameUI;

    public void RegisterUI(GameUI uiType, IGameUI uiToRegister)
    {
        registeredUIs.Add(uiType, uiToRegister);
    }

    private void Awake()
    {
        foreach(IGameUI enumeratedUI in UIContainer.GetComponentsInChildren<IGameUI>(true))
        {
            RegisterUI(enumeratedUI.GetUIType(), enumeratedUI);
        }

        ShowUI(startingGameUI); // TODO -> SHOW MAIN MENU FIRST
    }

    public void ShowUI(GameUI uiType)
    {
        foreach(KeyValuePair<GameUI, IGameUI> kvp in registeredUIs)
        {
            kvp.Value.SetActive(kvp.Key == uiType);
        }

        currentActiveUI = uiType;
    }

    public GameUI GetCurrentActiveUI()
    {
        return currentActiveUI;
    }
}