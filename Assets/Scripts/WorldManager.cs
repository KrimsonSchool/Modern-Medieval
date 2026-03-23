using TMPro;
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

    public GameObject playerSpawn;
    public GameObject[] enemies;

    public GameObject[] worldObjects;

    public GameObject[] scenarios;
    
    public TextMeshProUGUI scenarioText;
    
    GameObject player;
    void Start()
    {
        playerSpawn = GameObject.Find("PlayerSpawn");
        if (playerSpawn == null)
        {
            playerSpawn = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
            playerSpawn.name = "PlayerSpawn";
        }
        if (FindFirstObjectByType<PlayerMovement>() == null)
        {
            player = Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
        }
        
        int rng = Random.Range(0, scenarios.Length);
        string scenarioType = "";
        switch (rng)
        {
            case 0:
                scenarioType = "Enemy";
                break;
            case 1:
                scenarioType = "Puzzle";
                break;
        }

        scenarioText.text = scenarioType + " type layer.";
        scenarios[rng].SetActive(true);
        
        Destroy(playerSpawn.gameObject);
    }

    void Update()
    {
    }
}