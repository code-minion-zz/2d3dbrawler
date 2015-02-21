using UnityEngine;
using System.Collections;

public class CatAnimator : MonoBehaviour {

    private SpriteRenderer renderer;
    private SpriteManager.SpriteAnimation spriteSheet;
    private int currentFrame = 0;
    private int maxFrames;
    private float frameAccum = 0;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.grey;
        spriteSheet = SpriteManager.Instance.GetAnimation("Idle");
        maxFrames = spriteSheet.sprites.Count;
	}
	
	// Update is called once per frame
	void Update () {
        frameAccum += Time.deltaTime;

        if (frameAccum > spriteSheet.frameDelay)
        {
            frameAccum = 0;
            ++currentFrame;
            if (currentFrame >= maxFrames)
            {
                currentFrame = 0;
            }
            renderer.sprite = spriteSheet.sprites[currentFrame];
        }
	}
}
