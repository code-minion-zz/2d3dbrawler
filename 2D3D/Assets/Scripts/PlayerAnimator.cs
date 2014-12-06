using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

    bool carrying = false;
    Animator animator;

    public bool Carrying {
        get {
            return carrying;
        }
        set {
            carrying = value;
            animator.SetBool("HaveBall", value);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnAnimationComplete(string anim)
    {
        Debug.Log(anim + " complete");

        if (anim == "throw")
        {
            Carrying = false;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.name == "Field") return;

        if (other.name == "")
        {

        }
    }
}
