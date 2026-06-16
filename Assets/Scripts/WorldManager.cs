using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject[] differences;
    public GameObject dialogueBox;
    public GameObject interactUI;

    [HideInInspector] public GameObject interactedObject;

    public GameObject playerPrefab;
    public GameObject hurtEffect;

    public Slider healthSlider;
    public Slider cooldownSlider;
    public Slider xpSlider;

    public GameObject playerSpawn;
    public bool playerHasWeapon;
    public bool playerHasPDA;

    public GameObject[] enemies;

    public GameObject[] worldObjects;

    public GameObject[] scenarios;

    public TextMeshProUGUI scenarioText;
    public TextMeshProUGUI interactText;

    public GameObject detectedIndicator;
    GameObject player;

    public Color[] gorbachevTheOmnisiah;

    private void Awake()
    {
        gorbachevTheOmnisiah = new Color[6];
        gorbachevTheOmnisiah[0] = Color.red;
        gorbachevTheOmnisiah[1] = Color.blue;
        gorbachevTheOmnisiah[2] = Color.green;
        gorbachevTheOmnisiah[3] = Color.yellow;
        gorbachevTheOmnisiah[4] = Color.cyan;
        gorbachevTheOmnisiah[5] = Color.magenta;
    }

    void Start()
    {
        //playerSpawn = GameObject.Find("PlayerSpawn");
        if (playerSpawn == null)
        {
            print("playerSpawn is null");
            playerSpawn = GameObject.Find("PlayerSpawn");
            if (playerSpawn == null)
            {
                playerSpawn = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
                playerSpawn.name = "PlayerSpawn";
            }
        }

        if (FindFirstObjectByType<PlayerMovement>() == null)
        {
            //print("Spawning at: " + playerSpawn.transform.position +" pos");
            player = Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
            player.transform.position = playerSpawn.transform.position;
            player.transform.rotation = playerSpawn.transform.rotation;

            player.GetComponent<PDA>().enabled = playerHasPDA;
            player.GetComponent<PlayerWeapons>().enabled = playerHasWeapon;
        }

        /*
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
        */
        Destroy(playerSpawn.gameObject);
    }

    void Update()
    {
    }
}