using System;
using InputSystemGlobal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions controls;
    private void Awake() => controls = new InputSystem_Actions();

    public float speed;
    public float jumpAmount;
    public float mouseSpeed;
    public Camera cam;

    private Vector2 move;
    private Vector2 mouse;

    private float _rotX;
    public float interactDist;

    private PlayerHolder playerHold;
    
    public GameObject heldObject;
    public GameObject holdPos;

    private PlayerInput playerInput;

    [HideInInspector] public WorldManager worldManager;

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        worldManager = FindFirstObjectByType<WorldManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controls.Player.Jump.started += ctx => Jump();
        controls.Player.Interact.started += ctx => Interact();
        
        controls.Player.Exit.started += ctx => Exit();
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = holdPos.transform.position;
        }
        
        if (playerHold == null)
        {
            playerHold = GetComponent<PlayerHolder>();
        }
        
        if (worldManager == null)
        {
            worldManager = playerHold.worldManager;
        }

        PollInput();

        transform.position += transform.forward * (speed * Time.deltaTime * move.y)
                              + transform.right * (speed * Time.deltaTime * move.x);

        transform.Rotate(0, mouse.x * mouseSpeed * Time.deltaTime, 0);

        _rotX -= mouse.y * mouseSpeed * Time.deltaTime;
        _rotX = Mathf.Clamp(_rotX, -60f, 60f);
        cam.transform.localRotation = Quaternion.Euler(_rotX, 0, 0);

        if (transform.position.y < -10f)
        {
            GetComponent<Health>().Hurt(2147483647);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, interactDist))
        {
            //interaction ui popup
            //Need array of Interactable tags
            if (hit.collider.CompareTag("Npc") || hit.collider.CompareTag("Door") || hit.collider.CompareTag("Switch"))
            {
                worldManager.interactUI.SetActive(true);
                worldManager.interactedObject = hit.collider.gameObject;
            }
        }
        else
        {
            if (worldManager == null)
            {
                print("ERROR!!!!!!!!!1");
            }
            if (worldManager.interactUI.activeSelf)
                worldManager.interactUI.SetActive(false);

            worldManager.interactedObject = null;
        }

        if (playerInput.actions["Jump"].triggered)
        {

        }
    }

    private void PollInput()
    {
        move = controls.Player.Move.ReadValue<Vector2>();
        mouse = controls.Player.Look.ReadValue<Vector2>();
    }

    public void Jump()
    {
        LayerMask mask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var hit, 1.1f, mask))
        {
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.down) * hit.distance, Color.red);

            print(hit.distance);

            GetComponent<Rigidbody>().AddForce(transform.up * jumpAmount, ForceMode.Impulse);
        }
    }

    public void Interact()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, interactDist))
        {
            //print(hit.collider.tag);
            switch (hit.collider.tag)
            {
                case "Npc":
                    FindFirstObjectByType<WorldManager>().dialogueBox.SetActive(true);
                    FindFirstObjectByType<WorldManager>().dialogueBox.GetComponent<DialogueManager>().currentDialogue =
                        hit.collider.gameObject.GetComponent<Dialogue>();
                    FindFirstObjectByType<WorldManager>().dialogueBox.GetComponent<DialogueManager>().IncrementDialogue();
                    break;
                case "Switch":
                    hit.collider.gameObject.GetComponent<Switch>().SwitchState();
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MObject"))
        {
            if(heldObject==null)
            {
                heldObject = other.gameObject;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}