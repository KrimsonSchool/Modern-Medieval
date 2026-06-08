using UnityEngine;

public class SoundBlaster98 : MonoBehaviour
{
    //TODO: make this not destroy on load, load in song based on level w/ smooth transition
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().volume = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
