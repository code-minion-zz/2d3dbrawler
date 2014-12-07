using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

    public Sprite[] snowmanSprites;
    uint _hp = 3;
    public uint HP
    {
        get { return _hp; }
        set 
        {
            _hp = value;
        }
    }
    public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = snowmanSprites[_hp];
	}
	
    public void Hit() {
        // decrease HP, swap sprite
        --_hp;
        spriteRenderer.sprite = snowmanSprites[_hp];

        if (_hp <= 0) {
            //snowman dead
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Ball"))
        {
            // explode, decrease hp
            Hit();
            Game.Instance.PlayExplosion();
            Destroy(other.gameObject);
            Game.Instance.BallDestroyed();
        }
    }
}
