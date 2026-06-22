using InputSystemGlobal;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{      
    private InputSystem_Actions controls;
    private void Awake() => controls = new InputSystem_Actions();

    public TextAsset credits;
    public TextMeshProUGUI txtCredits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
    void Start()
    {
        txtCredits.text = credits.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Player.Exit.triggered)
        {
            gameObject.SetActive(false);
        }
    }
}
