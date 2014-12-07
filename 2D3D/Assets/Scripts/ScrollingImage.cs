using UnityEngine;
using System.Collections;

public class ScrollingImage : MonoBehaviour {

    public BallLogic ballLogic;
    public Transform ball;
    public Vector3 offset;
    Vector3 extraOffset = new Vector3(0f, 1f, 0f);

	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball").transform;
        ballLogic = ball.GetChild(0).GetComponent<BallLogic>();

        var temp = new Vector3(ball.position.x, ball.position.z, 0);
        offset = transform.position - temp;
	}
	
	// Update is called once per frame
    void Update()
    {
        var target = ball;
        Vector3 _extraOffset = Vector3.zero;

        if (ballLogic == null) return;

        if (!ballLogic.enabled)
        {
            target = ball.parent.parent.parent;
            _extraOffset = extraOffset;
        }

        float ballX = target.position.x;
        float ballZ = target.position.z;

        transform.position = Vector3.Slerp(transform.position, offset - new Vector3(ballX, ballZ) + _extraOffset, Time.deltaTime * 2);
        //transform.position = offset - new Vector3(ballX, ballZ) + _extraOffset;
	}
}
