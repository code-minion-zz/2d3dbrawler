using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
        
    PlayerAnimator _animator;

	// Use this for initialization
	void Start () {
        _animator = transform.GetChild(0).GetComponent<PlayerAnimator>();
	}
	
	// Update is called once per frame
	void Update () {	    
	}

}
