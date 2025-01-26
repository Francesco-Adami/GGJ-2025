using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuUI : MonoBehaviour, IGameUI
{
    public UIManager.GameUI type;
    //public SettingsMenuUI settingsMenu;
    //private GameObject openedBy;
    public UIManager.GameUI GetUIType()
    {
        return type;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /*
    public void GetBack() 
    {
        openedBy.SetActive(true);
    }

    public void SetOpenedBy()
    {
        openedBy = settingsMenu.GetOpenedBy();
    }*/
}
