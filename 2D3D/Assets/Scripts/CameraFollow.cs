using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public BallLogic ball;
	public Transform target;
    public Transform tempTarget;
    public Vector3 distance;
    Vector3 extraOffset = new Vector3(0f, 1f, 0f);
    Vector3 Offset = new Vector3(0,0,-1);

	// Use this for initialization
	void Start () {
        ball = target.GetChild(0).GetComponent<BallLogic>();
        //distance = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 result = new Vector3(target.position.x,target.position.y,target.position.z) + distance;
        Vector3 result;

        if (ball == null) return;
        if (!ball.enabled)
        {
            //target.parent
            tempTarget = target.parent.parent.parent;
            result = tempTarget.position + (distance + extraOffset);
        }
        else
        {
            result = target.position + distance;
        }

        transform.position = Vector3.Slerp(transform.position, result, Time.deltaTime) ;
	}
}
