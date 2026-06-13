using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public TextAsset credits;
    public TextMeshProUGUI txtCredits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txtCredits.text = credits.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
