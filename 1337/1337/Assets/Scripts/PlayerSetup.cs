using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField] //Private but viewable in Unity
    Behaviour[] componentsToDisable;

    Camera sceneCam;

    void Start()
    {

        // if The player is not the "Local Player" ignore the imputs for movement
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            // if the scene camera is active, make it not!
            sceneCam = Camera.main;
            if (sceneCam != null)
            {
                sceneCam.gameObject.SetActive(false);
            }
        }
        
    }


    //if the scene camera is not active, make it so!
    void OnDisable()
    {
      if(sceneCam != null)
        {
            sceneCam.gameObject.SetActive(true);
        }  
    }


}
