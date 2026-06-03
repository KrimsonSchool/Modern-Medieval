using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public int detectionPercent;
    public Light light;

    [HideInInspector] public bool hasDetected;

    Animator anim;

    public float detectSpeed = 1;
    public float searchSpeed = 1;
    public int type = 0;

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
        if (detectionPercent < 1)
        {
            light.color = Color.white;
        }
        else if (detectionPercent < 75/detectSpeed)
        {
            light.color = Color.yellow;
        }
        else if (detectionPercent < 100/detectSpeed)
        {
            light.color = Color.red;
        }

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