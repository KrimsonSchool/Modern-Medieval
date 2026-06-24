using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public int detectionPercent;
    public Light light;

    [HideInInspector] public bool hasDetected;

    Animator anim;

    public int detectSpeed = 1;
    public float searchSpeed = 1;
    public int type = 0;

    [Space]
    public bool alerter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        
        anim.SetInteger("Type", type);
        anim.speed = searchSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        light.color = new Color(1f, 1f - (detectionPercent / 100f), 1f - (detectionPercent / 100f), 1f);
        
        if (!hasDetected)
        {
            anim.StopPlayback();
        }
        else
        {
            anim.StartPlayback();
        }
        
        
    }
}