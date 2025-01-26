using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Cam sensibility")]
    [SerializeField] private float ySense;

    private float xRotation;
    private float yRotation;

    [Header("Transform")]
    public Transform gun;
    public Transform head;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.isGameStarted) return;
        
        float YaxisRotation = Input.GetAxis("Mouse Y") *Time.fixedDeltaTime * ySense;
        xRotation -= YaxisRotation;
        xRotation = Mathf.Clamp(xRotation, -40f, 40f);
        gun.localRotation = Quaternion.Euler(0, xRotation, -90f);
        head.localRotation = Quaternion.Euler(0, 0, -xRotation);
    }
}
