using InputSystemGlobal;
using UnityEngine;

public class CursorPos : MonoBehaviour
{
    private InputSystem_Actions controls;
    private void OnEnable() => controls.UI.Enable();
    private void OnDisable() => controls.UI.Disable();

    [Tooltip("Drag your Canvas or the parent UI panel here")]
    public RectTransform parentCanvasRect;

    [Tooltip("Drag the Camera rendering the UI here. Leave null if Canvas is Screen Space - Overlay")]
    public Camera uiCamera;

    private RectTransform cursorRect;

    // Assuming you have your controls set up here or passed in
    // private PlayerControls controls; 

    private void Awake()
    {
        controls = new InputSystem_Actions();
        cursorRect = GetComponent<RectTransform>();
        
        Cursor.visible = false;
    }

    private void Update()
    {
        // 1. Get the screen position from the Input System
        Vector2 screenPosition = controls.UI.Point.ReadValue<Vector2>();

        // 2. Convert the screen position to a local point within the Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvasRect,
            screenPosition,
            uiCamera,
            out Vector2 localPoint
        );

        // 3. Apply the converted position to the cursor's local position
        cursorRect.localPosition = localPoint;
    }
}