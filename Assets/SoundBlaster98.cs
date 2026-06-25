using UnityEngine;

public class SoundBlaster98 : MonoBehaviour
{
    //TODO: make this not destroy on load, load in song based on level w/ smooth transition

    private AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerSound(AudioClip clip)
    {
        audio.Stop();
        audio.PlayOneShot(clip);
    }
}
