using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
    public Vector3 distance;
    Vector3 Offset = new Vector3(0,0,-1);

	// Use this for initialization
	void Start () {
        distance = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 result = new Vector3(target.position.x,target.position.y,target.position.z) + distance;
		transform.position = result;
	}
}
