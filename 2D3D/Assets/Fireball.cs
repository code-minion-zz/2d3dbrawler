using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    Vector3 left = new Vector3(-1, 1, 1);
    Vector3 right = new Vector3(1, 1, 1);

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnFireOver()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
    
    public void FlipFacing(bool _left)
    {
        if (_left)
        {
            transform.localScale = left;
        }
        else
        {
            transform.localScale = right;
        }
    }
}
