using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour, IGameUI
{
    public UIManager.GameUI type;
    //private GameObject openedBy;
    public UIManager.GameUI GetUIType()
    {
        return type;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetMusicVolume(Slider slider) 
    {
        SoundManager.Instance.SetMusicVolume(slider.value);
    }

    public void SetFPXVolume(Slider slider)
    {
        SoundManager.Instance.SetMusicVolume(slider.value);
    }

    public void SetFullScreenMode(Toggle mode) 
    {
        Screen.fullScreen = mode.isOn;
    }

    /*public void SetOpenedBy(GameObject obj) 
    {
        openedBy = obj;
    }

    public GameObject GetOpenedBy()
    {
        return openedBy;
    }*/
}