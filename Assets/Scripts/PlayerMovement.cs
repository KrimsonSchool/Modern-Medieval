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

    //private PlayerInput playerInput;

    [HideInInspector] public WorldManager worldManager;

    private bool spotted;
    private float spottedTimer;
    SecurityCamera securityCamera;

    private float movSpeed;
    private float runSpeed;

    private PDA pda;

    [Space] public SoundBlaster98 sound;

    private PlayerHolder pholder;

    private bool startRun;

    private int isJumping;

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
        pholder = GetComponent<PlayerHolder>();

        runSpeed = speed * 1.5f;
        movSpeed = speed;
        //DontDestroyOnLoad(gameObject);

        worldManager = FindFirstObjectByType<WorldManager>();
        pda = GetComponent<PDA>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //controls.Player.Jump.started += ctx => Jump();
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
        
        //TODO use velocity movement instead of positional

        transform.position += transform.forward * (movSpeed * Time.deltaTime * move.y)
                              + transform.right * (movSpeed * Time.deltaTime * move.x);

        if (move.x != 0 || move.y != 0)
        {
            if (!startRun)
            {
                startRun = true;
                sound.TriggerSound(worldManager.sounds[1]);
            }

            if (!sound.GetComponent<AudioSource>().isPlaying)
            {
                startRun = false;
            }
            //sound.TriggerSound(pholder.sounds[1]);
        }

        transform.Rotate(0, mouse.x * mouseSpeed * Time.deltaTime, 0);

        _rotX -= mouse.y * mouseSpeed * Time.deltaTime;
        _rotX = Mathf.Clamp(_rotX, -60f, 60f);
        cam.transform.localRotation = Quaternion.Euler(_rotX, 0, 0);

        if (transform.position.y < -10f)
        {
            GetComponent<Health>().Hurt(2147483647);
        }

        Debug.DrawLine(cam.transform.position, cam.transform.position + cam.transform.TransformDirection(Vector3.forward) * 1, Color.red);

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out var hit, interactDist))
        {
            //print("HIT " + hit.collider.tag);
            //interaction ui popup
            //Need array of Interactable tags
            if (hit.collider.CompareTag("Npc") || hit.collider.CompareTag("Door") || hit.collider.CompareTag("Switch") || hit.collider.CompareTag("Lock") || hit.collider.CompareTag("Pickup"))
            {
                worldManager.interactUI.SetActive(true);
                worldManager.interactedObject = hit.collider.gameObject;
                
                if (hit.collider.CompareTag("Lock"))
                {
                    string typeName = hit.collider.gameObject.GetComponent<DoorOpener>().typeName;
                    worldManager.interactText.text = "Press [" + controls.Player.Interact.GetBindingDisplayString(0) + "]\n required " + typeName + ": [" +
                                                     FindFirstObjectByType<WorldManager>().gorbachevTheOmnisiah[hit.collider.GetComponent<DoorOpener>().requiredID].name + "]";
                }
                if (hit.collider.CompareTag("Pickup"))
                {
                    worldManager.interactText.text = "Press [" + controls.Player.Interact.GetBindingDisplayString(0) + "] to pickup\n" + hit.collider.gameObject.name;
                }
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

        if (spotted)
        {
            spottedTimer += Time.deltaTime;

            if (spottedTimer >= 0.1f)
            {
                spottedTimer = 0;
                securityCamera.detectionPercent += 1 + securityCamera.detectSpeed;
            }

            if (securityCamera.alerter)
            {
                if (securityCamera.detectionPercent >= 100)
                {
                    GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (var e in allEnemy)
                    {
                        e.GetComponent<Enemy>().chase = true;
                    }
                }
            }
            else
            {
                if (securityCamera.detectionPercent >= 100)
                {
                    GetComponent<PlayerHealth>().Hurt(999);
                }
            }
        }

        if (controls.Player.Next.triggered)
        {
            pda.selected++;
        }

        if (controls.Player.Previous.triggered)
        {
            pda.selected--;
        }

        if (controls.Player.Sprint.inProgress)
        {
            movSpeed = runSpeed;
        }
        else
        {
            if (movSpeed == runSpeed)
            {
                movSpeed = speed;
            }
        }

        if (controls.Player.Change.triggered)
        {
            pda.up = !pda.up;
        }
        
        LayerMask mask = LayerMask.GetMask("Ground");
        float detDis = 1.1f;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var ground, detDis, mask))
        {
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.down) * detDis, Color.red);
            
            isJumping --;
            //TODO make so can jump out of moving platform...
            if (controls.Player.Jump.triggered)
            {
                Jump();
            }
            
            if (ground.collider.gameObject.GetComponent<MovingPlatform>() != null && isJumping<1)
            {
                GetComponent<Rigidbody>().linearVelocity = ground.collider.gameObject.GetComponent<Rigidbody>().linearVelocity;
            }
            
        }
    }

    private void PollInput()
    {
        move = controls.Player.Move.ReadValue<Vector2>();
        mouse = controls.Player.Look.ReadValue<Vector2>();
    }

    public void Jump()
    {
        //.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.down) * hit.distance, Color.red);

        //print(hit.distance);

        isJumping = 100;

        sound.TriggerSound(worldManager.sounds[0]);
        GetComponent<Rigidbody>().linearVelocity = new Vector3(GetComponent<Rigidbody>().linearVelocity.x, 0, GetComponent<Rigidbody>().linearVelocity.z);
        GetComponent<Rigidbody>().AddForce(transform.up * jumpAmount, ForceMode.Impulse);
    }

    public void Interact()
    {
        //print("Pressed Interact " + Time.time);
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out var hit, interactDist))
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
                case "Lock":
                    hit.collider.gameObject.GetComponent<DoorOpener>().TryOpenDoor();
                    break;
                case "Pickup":
                    hit.collider.gameObject.GetComponent<Pickup>().PickupItem();
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MObject"))
        {
            if (heldObject == null)
            {
                heldObject = other.gameObject;
            }
        }

        if (other.CompareTag("Key"))
        {
            GetComponent<PlayerInventory>().AddItem(other.gameObject.GetComponent<Key>().obj);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Detector"))
        {
            worldManager.detectedIndicator.SetActive(true);
            worldManager.detectedIndicator.GetComponent<Animator>().Play(0);

            print("spotted");
            spotted = true;
            securityCamera = other.gameObject.GetComponentInParent<SecurityCamera>();
            securityCamera.hasDetected = true;
        }

        if (other.CompareTag("Kill"))
        {
            GetComponent<PlayerHealth>().Hurt(999);
        }

        if (other.CompareTag("TutorialArea"))
        {
            other.gameObject.GetComponent<TutorialArea>().Entered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            worldManager.detectedIndicator.SetActive(true);

            spotted = false;
            securityCamera.detectionPercent = 0;
            securityCamera.hasDetected = false;
            securityCamera = null;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}