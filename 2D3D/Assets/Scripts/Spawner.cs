using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject monster;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnMob());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator SpawnMob()
    {        
        while (true)
        {
            yield return new WaitForSeconds(3f);
            GameObject.Instantiate(monster,transform.position,transform.rotation);
        }
    }
}
