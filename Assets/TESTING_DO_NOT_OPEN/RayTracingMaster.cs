using UnityEngine;
using UnityEngine.UI; // Add this

public class RayTracingMaster : MonoBehaviour
{
    public ComputeShader rayTracingShader;
    public RawImage displayUI; // Drag a UI RawImage here
    public Vector4 sphereData = new Vector4(0, 0, 5, 1);
    
    private RenderTexture _target;
    private Camera _camera;

    void Start() {
        _camera = GetComponent<Camera>();
        InitRenderTexture();
    }

    void Update() // Use Update instead of OnRenderImage
    {
        if (rayTracingShader == null || displayUI == null) return;

        InitRenderTexture();

        // Set Shader Parameters
        int kernel = rayTracingShader.FindKernel("CSMain");
        rayTracingShader.SetTexture(kernel, "Result", _target);
        rayTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
        rayTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
        rayTracingShader.SetVector("_Sphere", sphereData);
        rayTracingShader.SetInt("_MaxBounces", 4);
// Use a fixed light direction (pointing down and away)
        Vector3 lDir = new Vector3(1, 1, -1).normalized;
        rayTracingShader.SetVector("_LightDirection", lDir);
        rayTracingShader.SetInt("_MaxBounces", 8);
        // Dispatch
        int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
        rayTracingShader.Dispatch(kernel, threadGroupsX, threadGroupsY, 1);

        // Display on UI
        displayUI.texture = _target;
    }

    private void InitRenderTexture() {
        if (_target == null || _target.width != Screen.width || _target.height != Screen.height) {
            if (_target != null) _target.Release();
            _target = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBFloat);
            _target.enableRandomWrite = true; // CRITICAL for Compute Shaders
            _target.Create();
        }
    }
}