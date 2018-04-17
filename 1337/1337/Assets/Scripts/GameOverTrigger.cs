using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            deathScreen.SetActive(true);
        }

    }


    public void restart()
    {

        SceneManager.LoadScene(Application.loadedLevel);

    }

    public void quit()
    {

        Application.Quit();

    }
}
