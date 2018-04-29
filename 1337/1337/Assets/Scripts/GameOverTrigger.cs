using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameOverTrigger : MonoBehaviour {

    public GameObject deathScreen;


    //for this to work both need colliders, one must have rigid body, and the enemy must have is trigger checked.
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "blast(Clone)")
        {
            //first disable the X's collider so multiple collisions cannot occur
            GetComponent<SphereCollider>().enabled = false;
            //destroy the bullet
            Destroy(col.gameObject);

            Debug.Log("Killed:" + this.gameObject.name);
            Destroy(gameObject);
            FindObjectOfType<PlayerCtrl>().gameObject.SetActive(false);
            FindObjectOfType<ShootingCtrl>().gameObject.SetActive(false);
            deathScreen.SetActive(true);
            Debug.Log("DeathScreen Active");

           // SceneManager.LoadScene(1);
            
        }

    }


 

    public void restart()
    {
        Debug.Log("RESTARTING");
        SceneManager.LoadScene(1);

    }

    public void quit()
    {
        Debug.Log("RESTARTING");
        Application.Quit();

    }
}
