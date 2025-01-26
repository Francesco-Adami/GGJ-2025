using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_FireRate : MonoBehaviour
{
    public int level;
    public float[] fireRate;

    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.SetFireRate(fireRate[level]);
        level++;
        if (level >= fireRate.Length)
        {
            PowerUpManager.Instance.RemovePUCompleted(gameObject);
        }
    }
}
