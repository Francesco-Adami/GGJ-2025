using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameMenuUI : MonoBehaviour, IGameUI
{
    public TextMeshProUGUI textMeshProUGUI;

    public UIManager.GameUI type;
    public UIManager.GameUI GetUIType()
    {
        return type;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void Update()
    {
        textMeshProUGUI.text = "Nemici Rimasti: " + EnemiesManager.Instance.enemiesSpawned;
    }
}
