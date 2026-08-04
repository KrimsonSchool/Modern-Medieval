using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Sensitivity")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;
    
    [Header("Audio")]
    public Slider audioSlider;
    public TextMeshProUGUI audioText;
    
    float sensitivity;
    float audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("sensitivity") <= 0)
        {
            PlayerPrefs.SetInt("sensitivity", 50);
        }
        if (PlayerPrefs.GetInt("audio") <= 0)
        {
            PlayerPrefs.SetInt("audio", 100);
        }
        
        sensitivitySlider.value =  PlayerPrefs.GetInt("sensitivity");
        audioSlider.value =  PlayerPrefs.GetInt("audio");
    }
    

    public void SetAudio()
    {
        audio = audioSlider.value;
        audioText.text = audio+"";
        PlayerPrefs.SetInt("audio", (int)audio);

        SetSource();
    }

    public void SetSensitivity()
    {
        sensitivity = sensitivitySlider.value;
        sensitivityText.text = sensitivity+"";
        PlayerPrefs.SetInt("sensitivity", (int)sensitivity);

        SetSource();
    }

    public void SetSource()
    {
        AudioSource[] aus = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource a in aus)
        {
            a.volume = PlayerPrefs.GetInt("audio")/100f;
        }
    }
}
