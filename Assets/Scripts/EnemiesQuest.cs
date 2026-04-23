using TMPro;
using UnityEngine;

public class EnemiesQuest : MonoBehaviour
{
    public int noOfEnemies;
    public int killedEnemies;
    public TextMeshProUGUI enemiesQuestText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemiesQuestText.text = "Enemies killed: " + killedEnemies +"/" + noOfEnemies;
        
        if (killedEnemies >= noOfEnemies)
        {
            GetComponent<DoorOpener>().OpenDoor();
        }
    }
}
