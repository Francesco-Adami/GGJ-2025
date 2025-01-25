using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PU_Dash : MonoBehaviour
{
    public void AcquirePowerUp()
    {
        PowerUpManager.Instance.SetDash();
    }
}
