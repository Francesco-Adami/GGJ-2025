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

    [Header("Transform")]
    public Transform point;
    public Transform gun;
    public Transform player;
    public Transform head;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = point.position;
        transform.rotation = point.rotation;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * xSense;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * ySense;

        yRotation += mouseY;
        xRotation += mouseX;


        //xRotation = Mathf.Clamp(xRotation,-90,90);
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        //player.rotation = Quaternion.Euler(0, xRotation, 0);
        player.Rotate(new Vector3(0, mouseX, 0));
        point.Rotate(new Vector3(-mouseY, 0, 0));
        gun.Rotate(new Vector3(0, 0, -mouseY));
        head.Rotate(new Vector3(0, 0, mouseY));
        //gun.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
    }
}
