using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xRotation;
    float yRotation;

    public Transform point;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = point.position;
        transform.rotation = point.rotation;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 100;

        yRotation += mouseY;
        xRotation += mouseX;


        //xRotation = Mathf.Clamp(xRotation,-90,90);
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        player.rotation = Quaternion.Euler(0, xRotation, 0);
        point.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
    }
}
