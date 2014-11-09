using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
    Vector3 Offset = new Vector3(0,0,-1);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 result = new Vector3(target.position.x,target.position.y,target.position.z) + Offset;
		transform.position = result;
	}
}
