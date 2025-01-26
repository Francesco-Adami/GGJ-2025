using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuUI : MonoBehaviour, IGameUI
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
}
