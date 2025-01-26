using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameMenuUI : MonoBehaviour, IGameUI
{
    public TextMeshProUGUI nemiciRimasti;
    public TextMeshProUGUI munizioni;
    public TextMeshProUGUI vitaSuVitaTot;

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
        nemiciRimasti.text = "Nemici Rimasti: " + EnemiesManager.Instance.enemiesSpawned;
        munizioni.text = PlayerManager.Instance.GetAvailableBullets() + " / " + PlayerManager.Instance.projectiles.Count;
        vitaSuVitaTot.text = PlayerManager.Instance.currentHealth + " / " + PlayerManager.Instance.maxHealth;
    }
}
