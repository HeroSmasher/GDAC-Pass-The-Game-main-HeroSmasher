using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{

    public float SenstivityX;
    public float SenstivityY;

    public Transform orientation;

     float XRotation;
     float YRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * SenstivityX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * SenstivityY;

        YRotation += mouseX;
        XRotation -= mouseY;
        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        orientation.rotation = Quaternion.Euler(0, YRotation, 0);


    }
}
