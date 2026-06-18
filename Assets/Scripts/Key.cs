using System;
using UnityEngine;


public class Key : MonoBehaviour
{
    public PlayerInventory.Object obj;

    private void Start()
    {                
        GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
        float ev100Value = 14f;
        float intensity = 0.125f * Mathf.Pow(2f, ev100Value); // Translates to Nits
        Color colour = FindFirstObjectByType<WorldManager>().gorbachevTheOmnisiah[obj.id];
        
        GetComponent<Renderer>().material.color = colour;
        GetComponent<Renderer>().material.SetColor("_EmissiveColor", colour * intensity);
    }
}