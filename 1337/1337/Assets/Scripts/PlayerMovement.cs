using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public Vector3 velocity = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets the Vector3 for Movement
    public void Move(Vector3 v)
    {
        velocity = v;   
    }

    //Gets the Vector3 for Rotation
    public void Rotate(Vector3 r)
    {
        rotation = r;
    }

    //Updates in Fixed Intervals
    void FixedUpdate()
    {
        PerformMove();
        PerformRotation();
    }

    //Performs Movement of Player

    public void PerformMove()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
    }

    //Performs Rotation of player Camera
    public void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        }
    }

}
