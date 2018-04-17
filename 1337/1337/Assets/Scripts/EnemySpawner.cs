using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemies;
    public Vector3 spawnVals;

    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    

	// Use this for initialization
	void Start () {
        StartCoroutine(waitSpawner());
	}
	
	// Update is called once per frame
	void Update () {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
	}

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            Vector3 spawnPostion = new Vector3(Random.Range(-spawnVals.x, spawnVals.x),1,Random.Range(-spawnVals.z,spawnVals.z));
            Instantiate(enemies, spawnPostion + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);


            yield return new WaitForSeconds(spawnWait);
        }

    }
}
