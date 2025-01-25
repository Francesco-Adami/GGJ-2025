using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Cam sensibility")]
    [SerializeField] private float xSense;
    [SerializeField] private float ySense;

    private float xRotation;
    private float yRotation;

    public Transform point;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = point.position;
        transform.rotation = point.rotation;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSense;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySense;

        yRotation += mouseY;
        xRotation += mouseX;


        //xRotation = Mathf.Clamp(xRotation,-90,90);
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        player.rotation = Quaternion.Euler(0, xRotation, 0);
        point.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
    }
}
