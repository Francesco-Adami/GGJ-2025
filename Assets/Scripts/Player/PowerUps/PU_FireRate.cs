using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_FireRate : MonoBehaviour
{
    public float fireRate;

    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.powerUpFireRate = this;
    }
}
