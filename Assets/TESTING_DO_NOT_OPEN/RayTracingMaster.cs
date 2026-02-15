using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RayTracingMaster : MonoBehaviour
{
    [Header("Resources")]
    public ComputeShader rayTracingShader;
    public RawImage displayUI;

    [Header("Scene Objects")]
    public List<MeshRenderer> sceneObjects;

    [Header("Settings")]
    [Range(0.1f, 1.0f)] public float renderScale = 0.5f; // Set to 0.5 for a 4x speed boost
    [Range(1, 8)] public int maxBounces = 2;
    public Vector3 lightDirection = new Vector3(1, 1, -1);

    private RenderTexture _target;
    private Camera _camera;
    private ComputeBuffer _triangleBuffer;
    private int _triangleCount;

    // Strict 128-byte alignment for Vulkan stability
    // (9 x Vector3 @ 12b) + (2 x float @ 4b) + (3 x float padding @ 12b) = 128 bytes
    struct Triangle
    {
        public Vector3 v0, v1, v2;      // 36 bytes
        public Vector3 n0, n1, n2;      // 36 bytes
        public Vector3 color;           // 12 bytes
        public float smoothness;        // 4 bytes
        public float metallic;          // 4 bytes
        public Vector3 boundsMin;       // 12 bytes
        public Vector3 boundsMax;       // 12 bytes
        public float p1, p2, p3;        // 12 bytes padding
    }

    private void Awake() => _camera = GetComponent<Camera>();
    private void OnDisable() => _triangleBuffer?.Release();
    private void Start()
    {
        sceneObjects = new List<MeshRenderer>(FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None));
    }
    private void Update()
    {
        if (rayTracingShader == null || displayUI == null) return;

        UpdateSceneBuffers();
        if (_triangleCount == 0 || _triangleBuffer == null) return;

        InitRenderTexture();

        int kernel = rayTracingShader.FindKernel("CSMain");

        // Bind GPU Variables
        rayTracingShader.SetTexture(kernel, "Result", _target);
        rayTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
        rayTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
        rayTracingShader.SetVector("_LightDirection", lightDirection.normalized);
        rayTracingShader.SetInt("_TriangleCount", _triangleCount);
        rayTracingShader.SetInt("_MaxBounces", maxBounces);
        rayTracingShader.SetBuffer(kernel, "_Triangles", _triangleBuffer);

        // Dispatch based on the RENDER TEXTURE size (not screen size)
        int threadGroupsX = Mathf.CeilToInt(_target.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(_target.height / 8.0f);
        rayTracingShader.Dispatch(kernel, threadGroupsX, threadGroupsY, 1);

        displayUI.texture = _target;
    }

    void UpdateSceneBuffers()
    {
        List<Triangle> allTris = new List<Triangle>();

        foreach (var renderer in sceneObjects)
        {
            if (renderer == null) continue;
            MeshFilter filter = renderer.GetComponent<MeshFilter>();
            if (filter == null || filter.sharedMesh == null) continue;

            Material mat = renderer.sharedMaterial;
            Color col = mat.HasProperty("_BaseColor") ? mat.GetColor("_BaseColor") : Color.white;
            float smooth = mat.HasProperty("_Smoothness") ? mat.GetFloat("_Smoothness") : 0.1f;
            float metal = mat.HasProperty("_Metallic") ? mat.GetFloat("_Metallic") : 0.0f;

            Vector3[] vertices = filter.sharedMesh.vertices;
            Vector3[] normals = filter.sharedMesh.normals;
            int[] indices = filter.sharedMesh.triangles;
            Matrix4x4 l2w = filter.transform.localToWorldMatrix;

            // Calculate World-Space AABB
            Bounds b = filter.sharedMesh.bounds;
            Vector3 bMin = l2w.MultiplyPoint3x4(b.min);
            Vector3 bMax = l2w.MultiplyPoint3x4(b.max);

            for (int i = 0; i < indices.Length; i += 3)
            {
                allTris.Add(new Triangle
                {
                    v0 = l2w.MultiplyPoint3x4(vertices[indices[i]]),
                    v1 = l2w.MultiplyPoint3x4(vertices[indices[i+1]]),
                    v2 = l2w.MultiplyPoint3x4(vertices[indices[i+2]]),
                    n0 = l2w.MultiplyVector(normals[indices[i]]),
                    n1 = l2w.MultiplyVector(normals[indices[i+1]]),
                    n2 = l2w.MultiplyVector(normals[indices[i+2]]),
                    color = new Vector3(col.r, col.g, col.b),
                    smoothness = smooth,
                    metallic = metal,
                    boundsMin = bMin,
                    boundsMax = bMax
                });
            }
        }

        _triangleCount = allTris.Count;
        if (_triangleCount == 0) return;

        if (_triangleBuffer == null || _triangleBuffer.count != _triangleCount)
        {
            _triangleBuffer?.Release();
            _triangleBuffer = new ComputeBuffer(_triangleCount, 128); 
        }
        _triangleBuffer.SetData(allTris);
    }

    private void InitRenderTexture()
    {
        int width = Mathf.Max(1, (int)(Screen.width * renderScale));
        int height = Mathf.Max(1, (int)(Screen.height * renderScale));

        if (_target == null || _target.width != width || _target.height != height)
        {
            _target?.Release();
            _target = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            _target.enableRandomWrite = true;
            _target.filterMode = FilterMode.Bilinear; // Smooths out the lower resolution
            _target.Create();
        }
    }
}