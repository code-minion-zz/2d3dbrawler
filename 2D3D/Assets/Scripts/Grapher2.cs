﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class Grapher2 : MonoBehaviour {

    [Range(10,100)]
	public int resolution = 10;
    private int currentResolution;

	private ParticleSystem.Particle[] points;
    private ParticleSystem particleSystem;

    public enum FunctionOption
    {
        Linear,
        Exponential,
        Parabola,
        Sine
    }
    public FunctionOption function;

    private delegate float FunctionDelegate3D(float x);
    private static FunctionDelegate3D[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine
	};

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (currentResolution != resolution || points == null)
        {
            CreatePoints();
        }
	    FunctionDelegate f = functionDelegates[(int) function];
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p = points[i].position;
            p.y = f(p.x);
            points[i].position = p;
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
        }
        particleSystem.SetParticles(points, points.Length);
	}

    void CreatePoints()
    {
        currentResolution = resolution;
        points = new ParticleSystem.Particle[resolution * resolution];
        float increment = 1f / (resolution - 1);
        int i = 0;
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                Vector3 p = new Vector3(x * increment, 0f, z * increment);
                points[i].position = p;
                points[i].color = new Color(p.x, 0f, p.z);
                points[i++].size = 0.1f;
            }
        }
    }

    private static float Linear(float x)
    {
        return x;
    }
    private static float Exponential(float x)
    {
        return x * x;
    }
    private static float Parabola(float x)
    {
        x = 2f*x - 1f;
        return x * x;
    }
    private static float Sine(float x)
    {
        return 0.5f + 0.5f*Mathf.Sin(2*Mathf.PI*x + Time.timeSinceLevelLoad);
    }
}
