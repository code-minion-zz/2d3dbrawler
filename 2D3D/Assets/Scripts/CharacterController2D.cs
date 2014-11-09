using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	Vector3 facing = new Vector3(0.5f,.5f,.5f);
    Vector3 velocity = Vector3.zero;
	bool facingLeft = false;
	float moveSpeed = 2.0f;
	public Animator animator;
    public BoxCollider leBox;
    float attack1Time = 0f;
    float attack2Time = 0f;
    const float ATTACK1DUR = .5f;
    const float ATTACK2DUR = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // 
		Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if (Input.anyKey) {
			if (inputVector.x < 0) {
				facingLeft = true;
				facing = new Vector3(-.5f,.5f,.5f);
			} else if (inputVector.x > 0){
				facingLeft = false;
				facing = new Vector3(.5f,.5f,.5f);
			}
		}
//		Debug.Log (inputVector.normalized.magnitude);
		transform.localScale = facing;
		Vector2 groundVelo = inputVector.normalized * moveSpeed * Time.fixedDeltaTime;
		//Debug.Log (groundVelo.magnitude);
		//rigidbody.AddForce(new Vector3(groundVelo.x, 0 , groundVelo.y), ForceMode.Acceleration);
        velocity = new Vector3(groundVelo.x, 0, groundVelo.y);

        Debug.Log(groundVelo.magnitude);

		animator.SetFloat("Speed",groundVelo.magnitude * 50);
		//Debug.Log ("rigidbody velo = " + rigidbody.velocity.magnitude);
	}

	void LateUpdate() {
		ProcessAttacks ();
	}

	void FixedUpdate() {
        attack1Time -= Time.fixedDeltaTime;
    //    Debug.Log(transform.position);
        transform.position += velocity;
//        Debug.Log(transform.position);
        velocity = Vector3.zero;
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
            attack1Time = ATTACK1DUR;
		}

//		transform.position += new Vector3(groundVelo.x, 0, groundVelo.y);
	}

    void OnDrawGizmos()
    {
        if (attack1Time > 0)
            Gizmos.DrawWireCube(leBox.bounds.center, leBox.bounds.size);
        //Gizmos.DrawCube(transform.position, leBox.size);
    }

    void OnTriggerEnter(Collider other)
    {
        if (attack1Time > 0)
        {
            Vector3 direction = (other.transform.position - transform.position);
            other.rigidbody.AddForce(direction * 1000);

            ActorController actor = other.GetComponent<ActorController>();

            if (actor != null)
            {
                actor.TakeDamage(2);
            }
        }
    }
}
