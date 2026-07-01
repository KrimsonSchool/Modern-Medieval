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
        Goals
    }

    public Menus menu;
    
    public TextMeshProUGUI title;
    public TextMeshProUGUI stack;
    public RawImage imgStack;

    [HideInInspector]
    public int selected;

    //[HideInInspector]
    //public bool stackLock;

    [HideInInspector] public bool up=true;

    public Vector3 pdaPosOrigin;
    public Vector3 pdaPosDown;
    //needs to contain: Inventory -> keys, Map, 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pda.SetActive(true);
        up = true;
        
        pdaPosOrigin = pda.transform.localPosition;
        pdaPosDown = pda.transform.localPosition + Vector3.down * 0.5f;
        
        pda.transform.localPosition = pdaPosDown;
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

        //if (!stackLock)
        //{
            imgStack.enabled = false;
            //stack.text = "";
            //print("RESAINFG");
            
            
            if (menu == Menus.Home)
            {
                stack.text = "";
                //stackLock = true;
                stack.text = "Press [E] or [Q] to cycle menus";
            }
            else if (menu == Menus.Map)
            {
                stack.text = "";
                //stackLock = true;
                imgStack.enabled = true;
            }
            else
            {
                //stackLock = false;
            }
        //}

        if (up)
        {
            if (pda.transform.localPosition != pdaPosOrigin)
            {
                pda.transform.localPosition = Vector3.MoveTowards(pda.transform.localPosition, pdaPosOrigin, Time.deltaTime);
                //pda.transform.localPosition = pdaPosOrigin;
            }
        }
        else
        {
            if (pda.transform.localPosition != pdaPosDown)
            {
                pda.transform.localPosition = Vector3.MoveTowards(pda.transform.localPosition, pdaPosDown, Time.deltaTime);
            }
        }

        
    }
}
