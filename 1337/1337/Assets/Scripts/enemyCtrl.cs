using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCtrl : MonoBehaviour
{

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
    }
}
