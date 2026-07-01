using TMPro;
using UnityEngine;

public class DEBUGMENUPICKER : MonoBehaviour
{
     TMP_InputField inputField;
     MenuManager menuManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField = FindFirstObjectByType<TMP_InputField>();
        menuManager = FindFirstObjectByType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DEBUG_TRYSCENE()
    {
        menuManager.OPEN_SCENE(inputField.text);
    }
}
