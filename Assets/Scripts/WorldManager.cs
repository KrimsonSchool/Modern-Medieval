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
            Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
        }
        
        Destroy(playerSpawn.gameObject);
    }

    void Update()
    {
    }
}