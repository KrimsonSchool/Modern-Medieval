using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;
    
    float sensitivity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("sensitivity") == 0)
        {
            PlayerPrefs.SetInt("sensitivity", 50);
        }
        
        sensitivitySlider.value =  PlayerPrefs.GetInt("sensitivity");
    }

    // Update is called once per frame
    void Update()
    {
        sensitivity = sensitivitySlider.value;
        sensitivityText.text = sensitivity+"";
        
        PlayerPrefs.SetInt("sensitivity", (int)sensitivity);
    }
}
