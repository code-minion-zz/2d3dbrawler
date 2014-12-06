using UnityEngine;
using System.Collections;

public class ScrollingImage : MonoBehaviour {

    Transform ball;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball").transform;

        var temp = new Vector3(ball.position.x, ball.position.z, 0);
        offset = transform.position - temp;
	}
	
	// Update is called once per frame
    void Update()
    {
        float ballX = ball.position.x;
        float ballZ = ball.position.z;

        transform.position = offset - new Vector3(ballX, ballZ);
	}
}
