using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUGMENUPICKER : MonoBehaviour
{
     TMP_InputField inputField;
     MenuManager menuManager;
     
     TMP_Dropdown dropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField = FindFirstObjectByType<TMP_InputField>();
        menuManager = FindFirstObjectByType<MenuManager>();

        dropdown = GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DEBUG_TRYSCENE()
    {
        menuManager.OPEN_SCENE(inputField.text);
    }

    public void SetScene()
    {
        SceneManager.LoadScene(dropdown.value);
    }
    
}
