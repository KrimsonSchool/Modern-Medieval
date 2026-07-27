using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Squimble : MonoBehaviour
{
    public bool squimble;
    public string targetName;
    public Vector3 diff;
    private List<GameObject> objects;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (squimble)
        {
            Sqimble();
        }
    }

    public void Sqimble()
    {
        squimble = false;
        objects = new List<GameObject>();
        GameObject[] all = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in all)
        {
            if (go.name == targetName)
                objects.Add(go);
        }

        foreach (GameObject obj in objects)
        {
            Undo.RecordObject(obj.transform, "Move Object");
            obj.transform.position += new  Vector3(Random.Range(-diff.x, diff.x), Random.Range(-diff.y, diff.y), Random.Range(-diff.z, diff.z));
        }
    }
}
