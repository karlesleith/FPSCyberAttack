using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginCtrl : MonoBehaviour {

    private string userName;
    private string password;

   

    ClientHandleData chd = new ClientHandleData();

    private void logIn()
    {
        
    }



    public void loadMainMenu()
    {
        SceneManager.LoadScene(1);
       
        
       
    }


}
