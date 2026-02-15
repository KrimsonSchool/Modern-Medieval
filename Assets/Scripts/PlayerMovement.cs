using InputSystemGlobal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions controls;
    private void Awake() => controls = new InputSystem_Actions();

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();


    public float speed;
    public float mouseSpeed;
    public Camera cam;

    private Vector2 move;
    private Vector2 mouse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PollInput();

        transform.position += transform.forward * (speed * Time.deltaTime * move.y)
                              + transform.right * (speed * Time.deltaTime * move.x);

        transform.Rotate(0, mouse.x * mouseSpeed * Time.deltaTime, 0);
        cam.transform.Rotate(-mouse.y * mouseSpeed * Time.deltaTime, 0, 0);
    }

    private void PollInput()
    {
        move = controls.Player.Move.ReadValue<Vector2>();
        mouse = controls.Player.Look.ReadValue<Vector2>();
    }
}