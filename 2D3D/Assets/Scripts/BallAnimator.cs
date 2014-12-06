using UnityEngine;
using System.Collections;

public class BallAnimator : MonoBehaviour {

    Rigidbody _rb;
    Collider _collider;
    SpriteRenderer _spr;

	// Use this for initialization
	void Start () {
        _rb = transform.parent.rigidbody;
        _spr = GetComponent<SpriteRenderer>();
        _collider = transform.parent.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float spinVelocity = _rb.angularVelocity.z;

        //Debug.Log(spinVelocity);
        float curZ = transform.rotation.z;
        curZ += (Time.fixedDeltaTime * spinVelocity * 4f);
        //transform.rotation.SetFromToRotation(transform.rotation.eulerAngles, new Vector3(0, 0, curZ));
        transform.Rotate(Vector3.forward * spinVelocity);
        //Debug.Log(transform.rotation.z);
	}

    public void Lock(bool doLock)
    {
        _rb.isKinematic = doLock;
        _collider.enabled = !doLock;
    }
}
