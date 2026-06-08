using InputSystemGlobal;
using UnityEngine;
using UnityEngine.InputSystem;


public class Door : MonoBehaviour
{
    private InputSystem_Actions controls;
    private void Awake() => controls = new InputSystem_Actions();
    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
    
    
    private MenuManager menu;
    PlayerMovement player;
    DoorManager doorManager;
    private WorldManager wm;

    public float interactDist;

    public string nextLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = FindFirstObjectByType<MenuManager>();
        player = FindFirstObjectByType<PlayerMovement>();
        doorManager = FindFirstObjectByType<DoorManager>();
        wm = FindFirstObjectByType<WorldManager>();
    }

    // Update is called once per frame
    void Update()
    {
        controls.Player.Interact.started += ctx => OpenDoor();
    }

    public void OpenDoor()
    {
        if(wm.interactedObject == gameObject)
        {
            print("Playing vid");
            StartCoroutine(doorManager.PlayVid(nextLevel));
        }
    }
}
