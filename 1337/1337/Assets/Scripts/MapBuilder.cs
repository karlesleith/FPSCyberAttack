using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapBuilder : MonoBehaviour {

	public void PlayMap()
    {
        Debug.Log("Debug: Playing user generated level");
    }
    public void SaveMap()
    {
        Debug.Log("Debug: Saving the User Level");
    }
    public void QuitToMain()
    {
        Debug.Log("Debug: Returning to MainMenu");
        SceneManager.LoadScene(0);
    }
   
}
