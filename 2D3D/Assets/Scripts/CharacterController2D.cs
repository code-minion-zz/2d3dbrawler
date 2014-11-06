using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	Vector3 facing = Vector3.one;
	bool facingLeft = false;
	float moveSpeed = 2.0f;
	public Animator animator;
    public BoxCollider leBox;
    float attack1Time = 0f;
    float attack2Time = 0f;
    const float ATTACK1DUR = 1f;
    const float ATTACK2DUR = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if (Input.anyKey) {
			if (inputVector.x < 0) {
				facingLeft = true;
				facing = new Vector3(-1,1,1);
			} else if (inputVector.x > 0){
				facingLeft = false;
				facing = new Vector3(1,1,1);
			}
		}
		Debug.Log (inputVector.normalized.magnitude);
		transform.localScale = facing;
		Vector2 groundVelo = inputVector.normalized * moveSpeed * Time.fixedDeltaTime * 500;
		//Debug.Log (groundVelo.magnitude);
		rigidbody.AddForce(new Vector3(groundVelo.x, 0 , groundVelo.y), ForceMode.Acceleration);
		
		animator.SetFloat("Speed",rigidbody.velocity.magnitude);
		//Debug.Log ("rigidbody velo = " + rigidbody.velocity.magnitude);
	}

	void LateUpdate() {
		ProcessAttacks ();
	}

	void FixedUpdate() {
        attack1Time -= Time.fixedDeltaTime;
	}
    
	void ProcessAttacks()
	{		
		if (Input.GetKeyDown (KeyCode.Z)) {
			animator.SetTrigger("Attack1");
            attack1Time = ATTACK1DUR;
            //Debug.Log(animator.cur);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			animator.SetTrigger("Attack2");
		}

//		transform.position += new Vector3(groundVelo.x, 0, groundVelo.y);
	}

	void OnDrawGizmos()
	{
        if (attack1Time > 0)
		Gizmos.DrawWireCube (leBox.bounds.center, leBox.size);
        //Gizmos.DrawCube(transform.position, leBox.size);
	}
}
