using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System.Collections.Generic;
using kf;

[ScriptedImporter(1, "md3")]
public class MD3Importer : ScriptedImporter
{
    public int frame = 0;
    private List<Mesh> allMesh = new(); 
    // Material material;

    public Texture2D texture;
    public override void OnImportAsset(AssetImportContext ctx)
    {
        byte[] data = System.IO.File.ReadAllBytes(ctx.assetPath);
        MemBlock mb = new MemBlock(data);

        int ident = mb.GetS32();
        int version = mb.GetS32();
        string name = mb.GetUTF8(64, false);
        int flags = mb.GetS32();
        int frameCount = mb.GetS32();
        int tagsCount = mb.GetS32();
        int surfacesCount = mb.GetS32();
        int skinsCount = mb.GetS32();
        int framesOf = mb.GetS32();
        int tagsOf = mb.GetS32();
        int surfacesOf = mb.GetS32();
        int eofOf = mb.GetS32();

        mb.Seek(surfacesOf);
        int identSurf = mb.GetS32();
        string nameSurf = mb.GetUTF8(64, false);
        int flagsSurf = mb.GetS32();
        int frameCountSurf = mb.GetS32();
        int shaderCountSurf = mb.GetS32();
        int vertCountSurf = mb.GetS32();
        int triCountSurf = mb.GetS32();
        int triOf = mb.GetS32();
        int shaderOf = mb.GetS32();
        int uvOf = mb.GetS32();
        int vertOf = mb.GetS32();

        for (int g = 0; g < frameCount; g++)
        { 
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new();
            List<Vector2> uvs = new();
            List<int> indices = new();
            List<Vector3> normals = new();
            mb.Seek(surfacesOf + vertOf + vertCountSurf * 8 * g);
            float scale = 1.0f / (64.0f * 64.0f);
            for (int i = 0; i < vertCountSurf; ++i)
            {
                float x = mb.GetS16() * scale;
                float y = mb.GetS16() * scale;
                float z = mb.GetS16() * scale;
                ushort normal = mb.GetU16();
                float lat = ((normal >> 8) & 255) * (2.0f * Mathf.PI) / 255.0f;
                float lng = (normal & 255) * (2.0f * Mathf.PI) / 255.0f;
                normals.Add(new Vector3(Mathf.Cos(lat) * Mathf.Sin(lng), Mathf.Cos(lng), Mathf.Sin(lat) * Mathf.Sin(lng)));
                vertices.Add(new Vector3(x, z, y));
            }

            mb.Seek(surfacesOf + triOf);
            for (int i = 0; i < triCountSurf; ++i)
            {
                int a = mb.GetS32();
                int b = mb.GetS32();
                int c = mb.GetS32();
                //Debug.Log("Tri: "+i+"  "+a+" "+b+" "+c);
                indices.Add(a);
                indices.Add(b);
                indices.Add(c);
            }

            mb.Seek(surfacesOf + uvOf);
            for (int i = 0; i < vertCountSurf; ++i)
            {
                float u = mb.GetFloat();
                float v = 1.0f - mb.GetFloat();
                uvs.Add(new Vector2(u, v));
            }

            Debug.Log("Ident: " + ident);
            Debug.Log("Version: " + version);
            Debug.Log("Frame Count: " + frameCount);
            Debug.Log("Tags Count: " + tagsCount);
            Debug.Log("Surfaces Count: " + surfacesCount);
            Debug.Log("Skins Count: " + skinsCount);
            Debug.Log("EOF Offset: " + eofOf);

            Debug.Log("Frame Count Surface: " + frameCountSurf);
            Debug.Log("Shader Count: " + shaderCountSurf);
            Debug.Log("Vert Count: " + vertCountSurf);
            Debug.Log("Tri Count: " + triCountSurf);

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.SetUVs(0, uvs);
            mesh.SetNormals(normals);

            mesh.name = "Mesh "+g;

            ctx.AddObjectToAsset("Mesh " + g, mesh);

            allMesh.Add(mesh);
        }
        
        GameObject gameObject = new GameObject();
        gameObject.name = name;
        gameObject.AddComponent<MeshFilter>().mesh = allMesh[frame];
        
        Material mat = new Material(Shader.Find("HDRP/Lit"));
        mat.SetTexture("_BaseColorMap", texture);

        gameObject.AddComponent<MeshRenderer>().material = mat;

        gameObject.AddComponent<MD3MeshAnimator>().meshFilter = gameObject.GetComponent<MeshFilter>();
        gameObject.GetComponent<MD3MeshAnimator>().meshes = allMesh;

        gameObject.GetComponent<MD3MeshAnimator>().animationStartFrames = new int[1];
        gameObject.GetComponent<MD3MeshAnimator>().animationStartFrames[0] = 0;
        gameObject.GetComponent<MD3MeshAnimator>().animationEndFrames = new int[1];
        gameObject.GetComponent<MD3MeshAnimator>().animationEndFrames[0] = frameCount-1;
        
        ctx.AddObjectToAsset("GameObject", gameObject);
        ctx.AddObjectToAsset("Material", mat);

        ctx.SetMainObject(gameObject);
    }
}