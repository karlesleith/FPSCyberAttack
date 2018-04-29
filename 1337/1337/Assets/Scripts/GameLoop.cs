using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour {

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
