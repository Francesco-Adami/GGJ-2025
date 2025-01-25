using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform focus;

    float xRotation;
    float yRotation;
    void Update()
    {
        transform.position = focus.position;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 100;

        yRotation += mouseY;
        xRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        transform.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
    }
}
