using UnityEngine;
using System.Collections;

public class ShadowLogic : MonoBehaviour {

    float accumulator;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        accumulator += Time.deltaTime;

        Vector3 parentPos = transform.parent.position;
        Ray ray = new Ray(parentPos, Vector3.down);
        RaycastHit hit;
        int mask = LayerMask.NameToLayer("Ground");
        Physics.Raycast(ray, out hit, 100f, mask);
        
        transform.position = Vector3.Lerp(transform.position, hit.point, accumulator) + offset;

        if (accumulator > 1f) accumulator = 1f;

        //Debug.Log(hit.distance);
	}

    void OnDrawGizmos()
    {
        Vector3 parentPos = transform.parent.position;
        Ray ray = new Ray(parentPos, Vector3.down);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
