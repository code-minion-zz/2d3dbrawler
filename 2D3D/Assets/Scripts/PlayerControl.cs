using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public BallLogic ballInHand;
    Transform _ballAnchor;
    Transform _facingIndicator;
    public GameObject _fireBall;
    Fireball _fireballLogic;
    PlayerAnimator _animator;
    Vector3 thisFrameMovement;
    Vector3 facing;
    bool thisFrameFire1 = false;    // attack, throw if have ball
    bool thisFrameFire2 = false;
    public bool isPlayer2 = false;
    float SPEEDMOD = 3f;
    float SPEEDMODWITHBALL = 2f;
    float TURNSPEED = 8f;

	// Use this for initialization
	void Awake () {
        _animator = transform.FindChild("Sprite").GetComponent<PlayerAnimator>();
        _ballAnchor = _animator.transform.FindChild("BallAnchor");
        //_facingIndicator = transform.FindChild("Facing mod");
        //_fireBall = transform.FindChild("Fireball").gameObject;
        _fireballLogic = _fireBall.GetComponent<Fireball>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!Game.GameStarted) return;

        string fire1 = "Fire1";
        string fire2 = "Fire2";
        string horizontal = "Horizontal";
        string vertical = "Vertical";

        if (isPlayer2)
        {
            horizontal = "Horizontal 2";
            vertical = "Vertical 2";
            fire1 = "JoystickFire1";
            fire2 = "JoystickFire2";
        }

        float _horizontal = Input.GetAxis(horizontal);
        float _vertical = Input.GetAxis(vertical);
        thisFrameMovement = new Vector3(_horizontal,0,_vertical).normalized;
        
        thisFrameFire1 = Input.GetButtonDown(fire1);
        thisFrameFire2 = Input.GetButtonDown(fire2);
	}

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        int currentAnimNameHash = _animator.animator.GetCurrentAnimatorStateInfo(0).nameHash;

        // if our State allows input
        if ((currentAnimNameHash == Game.IDLE_HASH) || (currentAnimNameHash == Game.CARRY_HASH))
        {
            // resolve movement for this frame
            if (thisFrameMovement != Vector3.zero)
            {
                float _SPEEDMOD = _animator.HaveBall ? SPEEDMODWITHBALL : SPEEDMOD;

                Vector3 newPos = transform.position + (thisFrameMovement * Time.fixedDeltaTime * _SPEEDMOD);
                transform.position = newPos;

                float step = TURNSPEED * Time.fixedDeltaTime;

                facing = Vector3.RotateTowards(transform.forward, thisFrameMovement, step, 0f);

                //facing = Vector3.Lerp(transform.forward, thisFrameMovement, Time.fixedDeltaTime);
                transform.forward = facing;

                //facing = transform.forward;

                if (FacingLeft())
                {
                    _animator.FlipFacing(true);
                    _fireballLogic.FlipFacing(true);
                    //if (ballInHand != null)
                    //{
                    //    ballInHand.transform.localScale = new Vector3(-1.2f, 1.2f, 1f);
                    //}
                }
                else
                {
                    _animator.FlipFacing(false);
                    _fireballLogic.FlipFacing(false);
                    //if (ballInHand != null)
                    //{
                    //    ballInHand.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                    //}
                }
            }

            if (thisFrameFire1)
            {
                //if (currentAnimNameHash == Game.CARRY_HASH)
                //{
                    // check for THROW/ATTACK input
                    _animator.ThrowOrFire();
                //}
            }

            //if (thisFrameFire2)
            //{
            //    if (currentAnimNameHash == Game.IDLE_HASH)
            //    {
            //        // check for THROW/ATTACK input
            //        _animator.ThrowOrFire();
            //    }
            //}
        }
    }

    bool FacingLeft()
    {
        var maxDot = -Mathf.Infinity;
        var closest = Vector3.zero;

        float dot = Vector3.Dot(transform.forward, Vector3.left);

        if (dot > maxDot)
        {
            closest = Vector3.left;
            maxDot = dot;
        }

        dot = Vector3.Dot(transform.forward, Vector3.right);

        if (dot > maxDot)
        {
            closest = Vector3.right;
            maxDot = dot;
        }


        if (closest == Vector3.left)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CaughtBall(Transform ball)
    {
        if (ball.childCount == 0) return;
        ballInHand = ball.GetChild(0).GetComponent<BallLogic>();
        ball.parent = _ballAnchor;
        ballInHand.Lock(true);
        ball.localPosition = Vector3.zero;
        _animator.HaveBall = true;
    }

    /** 
     * disconnects Ball from Player, returns reference to the BallLogic
     * component of the Ball we just disconnected
     * */
    public BallLogic LostBall()
    {
        BallLogic ball = ballInHand;
        _animator.HaveBall = false;
        ball.transform.parent.parent = null;
        ball.Lock(false);

        ballInHand = null;

        return ball;
    }

    public void Attack()
    {
        //activate collider for punching
        _fireBall.SetActive(true);        
        Game.Instance.PlayAttack();
    }

    public void Flinch()
    {
        _animator.StartFlinch();
        Game.Instance.PlayFall();
    }

    //public void Throw(BallLogic ball)
    //{
    //    //ball._rb
    //}
}
