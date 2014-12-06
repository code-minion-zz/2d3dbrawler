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
    public SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = snowmanSprites[_hp];
	}
	
    public void Hit() {
        --_hp;
        renderer.sprite = snowmanSprites[_hp];
        if (_hp <= 0) {
            //snowman dead
        }
    }
}
