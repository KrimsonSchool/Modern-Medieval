using System.Linq;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public bool[] code;
    public GameObject[] light;

    public bool[] state;
    public Switch[] switches;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<code.Length; i++)
        {
            if(Random.Range(0, 2) == 0)
            {
                code[i]=true;
            }
        }

        for (int i = 0; i < code.Length; i++)
        {
            light[i].SetActive(code[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            state[i] = switches[i].state;
        }
        
        if (state.SequenceEqual(code))
        {
            GetComponent<DoorOpener>().OpenDoor();
        }
    }
}
