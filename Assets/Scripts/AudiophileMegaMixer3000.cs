using UnityEngine;

public class AudiophileMegaMixer3000 : MonoBehaviour
{
    public AudioClip[] ambientSounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (AudioClip clip in ambientSounds)
        {
            GameObject glorpNode = new GameObject
            {
                transform =
                {
                    parent = transform
                },
                name = "Glorp Node"
            };
            glorpNode.AddComponent<AudioSource>().clip = clip;
            glorpNode.AddComponent<AudioSource>().volume = PlayerPrefs.GetInt("audio")/100f;
            glorpNode.GetComponent<AudioSource>().loop = true;
            glorpNode.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
