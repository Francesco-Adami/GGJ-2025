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
        
    }

    public void WaveFinished()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.PowerUps);
        isGameStarted = false;
    }
}
