using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject[] differences;
    public GameObject dialogueBox;
    public GameObject interactUI;

    public GameObject interactedObject;

    public GameObject playerPrefab;
    public GameObject hurtEffect;
    
    public Slider healthSlider;
    public Slider cooldownSlider;
    public Slider xpSlider;
    void Start()
    {
        if (FindFirstObjectByType<PlayerMovement>() == null)
        {
            Instantiate(playerPrefab);
        }
    }

    void Update()
    {
    }
}