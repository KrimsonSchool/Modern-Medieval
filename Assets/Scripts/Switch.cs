using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool state;

    public GameObject indicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        indicator.SetActive(state);
    }

    public void SwitchState()
    {
        state = !state;
    }
}
