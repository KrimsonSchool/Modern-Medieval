using TMPro;
using UnityEngine;

public class EnemiesQuest : MonoBehaviour
{
    public int noOfEnemies;
    public int killedEnemies;
    public TextMeshProUGUI enemiesQuestText;

    private PDA pda;

    WorldManager worldManager;

    [Space] SoundBlaster98 sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sound = FindFirstObjectByType<SoundBlaster98>();
        worldManager = FindFirstObjectByType<WorldManager>();
        pda = FindFirstObjectByType<PDA>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pda != null)
        {
            pda.title.text = "Enemies killed: " + killedEnemies + "/" + noOfEnemies;
        }
        else
        {
            pda = FindFirstObjectByType<PDA>();
        }

        if (killedEnemies >= noOfEnemies)
        {
            sound.TriggerSound(worldManager.sounds[12]);
            GetComponent<DoorOpener>().OpenDoor();
            enabled = false;
        }
    }
}