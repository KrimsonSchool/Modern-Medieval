using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public int detectionPercent;
    public Light light;

    [HideInInspector] public bool hasDetected;

    Animator anim;

    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectionPercent < 1)
        {
            light.color = Color.white;
        }
        else if (detectionPercent < 75/speed)
        {
            light.color = Color.yellow;
        }
        else if (detectionPercent < 100/speed)
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