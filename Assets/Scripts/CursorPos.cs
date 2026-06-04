using InputSystemGlobal;
using UnityEngine;

public class CursorPos : MonoBehaviour
{
    
    private InputSystem_Actions controls;
    private void Awake() => controls = new InputSystem_Actions();
    private void OnEnable() => controls.UI.Enable();
    private void OnDisable() => controls.UI.Disable();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = controls.UI.Point.ReadValue<Vector2>();
        
        print(controls.UI.Point.ReadValue<Vector2>());
    }
}
