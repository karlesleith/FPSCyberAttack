using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Random;

public class enemyCtrl : MonoBehaviour
{

    private int movespeed = 5;

    //for this to work both need colliders, one must have rigid body, and the enemy must have is trigger checked.
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "blast(Clone)")
        {
            //first disable the X's collider so multiple collisions cannot occur
            GetComponent<BoxCollider>().enabled = false;
            //destroy the bullet
            Destroy(col.gameObject);

            Debug.Log("Killed:" + this.gameObject.name);
            Destroy(gameObject);


        }

        if (col.gameObject.name.Contains("Cube"))
        {
            
            //  transform.Rotate(Vector3);
            Debug.Log("Ran into Wall");
            transform.Rotate(0,90,0);
        }

    }

   

    public void Update()
    {
        transform.Translate(Vector3.forward * movespeed * Time.deltaTime);

    }

}
