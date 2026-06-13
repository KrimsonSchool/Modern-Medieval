using TMPro;
using UnityEngine;

public class PDA : MonoBehaviour
{
    public enum  Menus
    {
        Home,
        Inventory,
        Map,
        Tutorials
    }

    public Menus menu;
    
    public TextMeshProUGUI title;
    public TextMeshProUGUI stack;

    [HideInInspector]
    public int selected;

    [HideInInspector]
    public bool stackLock;
    //needs to contain: Inventory -> keys, Map, 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        title.text = ">"+menu + " [" + selected + "]";

        if (selected > 3)
        {
            selected = 0;
        }
        else if (selected < 0)
        {
            selected = 3;
        }
        
        menu = (Menus)selected;

        if (!stackLock)
        {
            stack.text = "";
        }
    }
}
