using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using Assets.Scripts;

public class SpriteManager : MonoBehaviour {

    public static SpriteManager Instance;

    [Serializable]
    public class SpriteAnimation
    {
        public string name;
        public float frameDelay;
        public List<Sprite> sprites;
    }

    public List<SpriteAnimation> animations;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

	// Use this for initialization
	void Start () {    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public SpriteAnimation GetAnimation(string name)
    {
        return animations.Where(item=> item.name == name).First();
    }
}
