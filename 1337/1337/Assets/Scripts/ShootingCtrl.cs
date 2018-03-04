using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCtrl : MonoBehaviour {

    public WeaponsCtrl weapon;

    public Camera cam;

    //Ctrls what we are actually hitting when we fire the weapon
    public LayerMask mask;

    void Fire()
    {

        //Raycasting for when a weapon is firing from a gun 
        Debug.Log("Fire() Method has been entered");
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, weapon.weaponRange, mask))
        {
            Debug.Log(hit.collider.name + "Has Been Shot!");
        }
    }

    // Use this for initialization
    void Start () {
		
        if (cam == null)
        {
            Debug.Log("No Camera Active");
            this.enabled = false;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire One has been Pressed");
            Fire();
        }
	
	}
}
