using UnityEngine;

public class FaeQue : MonoBehaviour
{
    private DoorOpener papa;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        papa = GetComponentInParent<DoorOpener>();
        GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
        float ev100Value = 21f;
        float intensity = 0.125f * Mathf.Pow(2f, ev100Value); // Translates to Nits
        Color colour = FindFirstObjectByType<WorldManager>().gorbachevTheOmnisiah[papa.requiredID];
        
        GetComponent<Renderer>().material.SetColor("_EmissiveColor", colour * intensity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
