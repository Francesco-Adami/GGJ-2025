using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour, IGameUI
{
    public UIManager.GameUI type;
    private GameObject openedBy;
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
        SoundManager.Instance.SetSFXVolume(slider.value);
    }

    public void SetFullScreenMode(Toggle mode) 
    {
        Screen.fullScreen = mode.isOn;
    }

    public void SetMouseSensitivity(Slider slider) 
    {
        PlayerManager.Instance.mouseSensitivity *= slider.value;
    }

    public void SetOpenedBy(GameObject obj) 
    {
        openedBy = obj;
    }

    public void GetBack()
    {
        openedBy.SetActive(true);
    }

    /*
    public GameObject GetOpenedBy()
    {
        return openedBy;
    }*/
}