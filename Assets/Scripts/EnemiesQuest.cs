using TMPro;
using UnityEngine;

public class EnemiesQuest : MonoBehaviour
{
    public int noOfEnemies;
    public int killedEnemies;
    public TextMeshProUGUI enemiesQuestText;

    private PDA pda;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pda = FindFirstObjectByType<PDA>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pda.menu == PDA.Menus.Goals)
        {
            pda.stack.text = "";
            print("on goals menu");
            //pda.stackLock = true;
            print("stack was: "+pda.stack.text);
            pda.stack.text = "Enemies killed: " + killedEnemies +"/" + noOfEnemies;
            print("stack is now: "+pda.stack.text);
        }
        
        
        if (killedEnemies >= noOfEnemies)
        {
            GetComponent<DoorOpener>().OpenDoor();
        }
    }
}
