using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PDA : MonoBehaviour
{
    public GameObject pda;
    
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
    public RawImage imgStack;

    [HideInInspector]
    public int selected;

    [HideInInspector]
    public bool stackLock;
    //needs to contain: Inventory -> keys, Map, 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pda.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        title.text = ">"+menu;

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
            imgStack.enabled = false;
            stack.text = "";
            
            if (menu == Menus.Home)
            {
                stackLock = true;
                stack.text = "Press [E] or [Q] to cycle menus";
            }
            else if (menu == Menus.Tutorials)
            {
                stackLock = true;
                stack.text = "[WASD] to move\nMouse to look around";
            }
            else if (menu == Menus.Map)
            {
                stackLock = true;
                imgStack.enabled = true;
            }
            else
            {
                stackLock = false;
            }
        }

        
    }
}
