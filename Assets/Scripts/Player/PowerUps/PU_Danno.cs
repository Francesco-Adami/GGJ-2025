using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Danno : MonoBehaviour
{
    public int addDamage;

    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.powerUpDanno = this;
    }
}
