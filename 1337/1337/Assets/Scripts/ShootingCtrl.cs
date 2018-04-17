using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCtrl : MonoBehaviour {


    //Ctrl used for Shooting the  Blasts
    public WeaponsCtrl weapon;
    public GameObject spawnPoint;

    private bool isShooting; 

    public Camera cam;

    //Ctrls what we are actually hitting when we fire the weapon
    public LayerMask mask;

    void Fire()
    {

        //Raycasting for when a weapon is firing from a gun 
        Debug.Log("Fire() Method has been entered");
        RaycastHit hit;
        GameObject bullet = Instantiate(Resources.Load("blast", typeof(GameObject))) as GameObject;
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        BulletCtrl bc = bullet.GetComponent<BulletCtrl>();
        var boltSound = bullet.GetComponent<AudioSource>();
        boltSound.Play();
        // Debug.Log("Debug: Bullet Spawned");
        //Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.rotation = spawnPoint.transform.rotation;
        bullet.transform.position = spawnPoint.transform.position;
        
        //add force to the bullet in the direction of the spawnPoint's forward vector
        rb.AddForce(spawnPoint.transform.forward * 5000f);
        //destroy the bullet after 1 second
        Destroy(bullet, 3);
       
        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, weapon.weaponRange, mask))
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
