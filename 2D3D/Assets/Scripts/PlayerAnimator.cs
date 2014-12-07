using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

    bool haveBall = false;
    public Animator animator;
    PlayerControl controller;
    Vector3 left = new Vector3(-0.2f, 0.2f, 1f);
    Vector3 right = new Vector3(0.2f, 0.2f, 1f);
    float ballCooldown = 0.4f;
    bool ballCD = false;
    float cooldownAccumulator = 0f;

    public bool HaveBall {
        get {
            return haveBall;
        }
        set {
            haveBall = value;
            animator.SetBool("HaveBall", value);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
        controller = transform.parent.GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (ballCD)
        {
            cooldownAccumulator += Time.deltaTime;
            if (cooldownAccumulator > ballCooldown)
            {
                cooldownAccumulator = 0f;
                ballCD = false;
            }
        }
	}

    public void OnThrow()
    {
        BallLogic ball = controller.LostBall();
        ballCD = true;
        ball.Throw(controller.transform.forward);
    }

    public void OnAttack()
    {
        controller.Attack();
    }
    
    public void OnAnimationComplete(string anim)
    {
        Debug.Log(anim + " complete");

        //if (anim == "throw")
        //{
        //    BallLogic ball = controller.LostBall();
        //    justThrown = true;
        //    ball.Throw(controller.transform.forward);
        //}
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

    public void ThrowOrFire()
    {
        int currentState = animator.GetCurrentAnimatorStateInfo(0).nameHash;
        if (currentState == Game.IDLE_HASH)
        {
            animator.SetTrigger("Attack");
        }
        else if (currentState == Game.CARRY_HASH)
        {
            animator.SetTrigger("Throw");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Field") return;
        int currentState = animator.GetCurrentAnimatorStateInfo(0).nameHash;
        if (other.name.Contains("Ball"))
        {
            if (ballCD) return;

            if (currentState == Game.IDLE_HASH)
            {
                // we got da ball!
                if (!HaveBall)
                {
                    controller.CaughtBall(other.transform);
                }
            }
        }

        if (other.name.Contains("Fireball"))
        {
            if ((currentState == Game.IDLE_HASH)||(currentState == Game.CARRY_HASH))
            {
                controller.Flinch();

                if (HaveBall)
                {
                    controller.LostBall();
                    ballCD = true;
                }
            }
        }
    }

    public void StartFlinch()
    {
        animator.SetTrigger("Flinch");
    }
}
