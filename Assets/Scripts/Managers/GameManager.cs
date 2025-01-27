using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    #endregion

    public bool isGameStarted = false;

    public GameObject scenaStart;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            isGameStarted = false;
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.ShowUI(UIManager.GameUI.Pause);
        }

    }

    public void WaveFinished()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.PowerUps);
        Cursor.lockState = CursorLockMode.None;
        isGameStarted = false;
    }

    public void ResetAll()
    {
        PlayerManager.Instance.ReloadBullets();
        PowerUpManager.Instance.ResetPowerUp();
        EnemiesManager.Instance.ResetWaves();
    }

    public void PlayerDead()
    {
        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.ShowUI(UIManager.GameUI.Lose);
    }

    public void SetActiveStartScene(bool active)
    {
        scenaStart.SetActive(active);
    }
}
