using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
    public class PlayerCtrl : MonoBehaviour {

    //Gloabal Vars
    public float speed;
    public float sensitivity;
    private float xMovement;
    private float zMovement;

    private float yRot;
    private float xRot;


    Vector3 moveHorizontal;
    Vector3 moveVertical;
    Vector3 velocity;
    Vector3 rotation;

    
    private PlayerMovement motor;

    void Start()
    {
        motor = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        zMovement = Input.GetAxisRaw("Vertical");

        moveHorizontal = transform.right * xMovement;
        moveVertical = transform.forward * zMovement;
        velocity = (moveHorizontal + moveVertical).normalized * speed;

        motor.Move(velocity);

        //Calculate Rotation as a 3D Vector

        yRot = Input.GetAxisRaw("Mouse X");
        xRot = Input.GetAxisRaw("Mouse Y");

        rotation = new Vector3(0f, yRot ,0f) * sensitivity;

        motor.Rotate(rotation);
    }
}
