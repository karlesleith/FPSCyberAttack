using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour {

    public GameObject t;
    public int flickerTimer;



    //Flicker Timer for the Title
    void Flicker()
    {
        StartCoroutine(RemoveAfterSeconds(2, t));
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
      
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Debug: Game has Quit!");

    }
    public void MapEditor()
    {
        SceneManager.LoadScene(2);
    }

    
}
