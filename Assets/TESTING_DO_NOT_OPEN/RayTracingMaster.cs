using UnityEngine;

[ExecuteInEditMode]
public class RayTracingMaster : MonoBehaviour
{
    public ComputeShader rayTracingShader;
    public Vector4 sphereData = new Vector4(0, 0, 5, 1); // x,y,z, radius
    private RenderTexture _target;
    private Camera _camera;

    private void Awake() => _camera = GetComponent<Camera>();

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Render(destination);
    }

    private void Render(RenderTexture destination)
    {
        // Make sure we have a valid render target
        InitRenderTexture();

        // Set Shader Parameters
        rayTracingShader.SetTexture(0, "Result", _target);
        rayTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
        rayTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
        rayTracingShader.SetVector("_Sphere", sphereData);
        // How many times should the light bounce?
        rayTracingShader.SetInt("_MaxBounces", 8);

        // Dispatch the shader (run it on the GPU)
        int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
        rayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

        // Blit the result to the screen
        Graphics.Blit(_target, destination);
    }

    private void InitRenderTexture()
    {
        if (_target == null || _target.width != Screen.width || _target.height != Screen.height)
        {
            if (_target != null) _target.Release();
            _target = new RenderTexture(Screen.width, Screen.height, 0,
                RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            _target.enableRandomWrite = true;
            _target.Create();
        }
    }
}