using UnityEngine;
using System.Collections;

public class BallSounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        //Debug.Log("COLLIDED");
        foreach (var contact in coll.contacts)
        {
            //Debug.Log(contact.otherCollider.name);
            if ((contact.otherCollider.name == "Cube") || (contact.otherCollider.name == "Field"))
            {
                Game.Instance.PlayBounce();
            }
        }
    }
}
