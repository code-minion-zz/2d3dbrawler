using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static Game Instance;

    public static int IDLE_HASH = Animator.StringToHash("Base.idle");
    public static int ATTACK_HASH = Animator.StringToHash("Base.attack");
    public static int RISE_HASH = Animator.StringToHash("Base.rise");
    public static int THROW_HASH = Animator.StringToHash("Base.throw");
    public static int CARRY_HASH = Animator.StringToHash("Base.carry");

    public static bool GameStarted = false;
    float accumulator = 0f;
    Camera BGCam;

	// Use this for initialization
	void Awake () {
        if (Instance == null)
        {
            Instance = this;
        }
        BGCam = GameObject.Find("BGCam").GetComponent<Camera>();
	}

    void Start()
    {

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Time.time < 2f) return;
        accumulator += Time.fixedDeltaTime;
        //Debug.Log(GameStarted);
        if (GameStarted) return;
        
        BGCam.orthographicSize = Mathf.Lerp(10f, 3f, accumulator);
        camera.orthographicSize = Mathf.Lerp(10f, 3f, accumulator);

        //Debug.Log(Time.time);
        if (Time.time > 4f)
        {
            GameStarted = true;
        }
	}
}
