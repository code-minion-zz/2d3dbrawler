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

    Vector3 spawnPos = new Vector3(0f, 1f, 0f);
    public GameObject BallPrefab;
    public BallLogic Ball;

    bool ballDestroyed = false;
    CameraFollow camFollow;
    ScrollingImage scroll1, scroll2;

    public AudioClip[] sounds;

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
        camFollow = GetComponent<CameraFollow>();
        scroll1 = BGCam.transform.GetChild(0).GetComponent<ScrollingImage>();
        scroll2 = BGCam.transform.GetChild(1).GetComponent<ScrollingImage>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Time.time < 2f) return;
        accumulator += Time.fixedDeltaTime;

        if (ballDestroyed)
        {
            if (accumulator >= 1f)
            {
                RespawnBall();
                ballDestroyed = false;
            }
        }

        //Debug.Log(GameStarted);
        if (GameStarted) return;
        
        BGCam.orthographicSize = Mathf.Lerp(10f, 4f, accumulator);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(10f, 4f, accumulator);

        //Debug.Log(Time.time);
        if (Time.time > 4f)
        {
            GameStarted = true;
        }
	}

    public void BallDestroyed()
    {
        accumulator = 0f;
        ballDestroyed = true;
    }

    public void RespawnBall()
    {
        GameObject obj = (GameObject)GameObject.Instantiate(BallPrefab, spawnPos, Quaternion.identity);
        camFollow.target = obj.transform;
        camFollow.ball = obj.transform.GetChild(0).GetComponent<BallLogic>();
        scroll1.ball = obj.transform;
        scroll1.ballLogic = obj.transform.GetChild(0).GetComponent<BallLogic>();
        scroll2.ball = obj.transform;
        scroll2.ballLogic = obj.transform.GetChild(0).GetComponent<BallLogic>();
    }

    public void PlayExplosion()
    {
        int result = Random.Range(0, 1);
        AudioClip rng = sounds[result];

        GetComponent<AudioSource>().PlayOneShot(rng);
    }

    public void PlayAttack()
    {
        AudioClip clip = sounds[7];

        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void PlayBounce()
    {
        AudioClip clip = sounds[6];

        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void PlayFall()
    {
        int result = Random.Range(2, 5);
        AudioClip rng = sounds[result];

        GetComponent<AudioSource>().PlayOneShot(rng);
    }
}
