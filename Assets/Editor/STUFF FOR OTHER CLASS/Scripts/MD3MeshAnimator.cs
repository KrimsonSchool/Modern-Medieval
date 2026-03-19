using System;
using System.Collections.Generic;
using UnityEngine;

public class MD3MeshAnimator : MonoBehaviour
{
    public MeshFilter meshFilter;
    public List<Mesh> meshes;

    public float speed = 0.1f;
    private float timer;

    int meshIndex = 0;

    public int[] animationStartFrames;
    public int[] animationEndFrames;

    public int animation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshIndex = animationStartFrames[animation];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > speed)
        {
            meshFilter.mesh = meshes[meshIndex];
            meshIndex++;
            if (meshIndex >= animationEndFrames[animation])
            {
                meshIndex = animationStartFrames[animation];
            }
            timer = 0;
        }
    }
}