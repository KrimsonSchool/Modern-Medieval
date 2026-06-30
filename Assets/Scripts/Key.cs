using System;
using UnityEngine;


public class Key : MonoBehaviour
{
    public PlayerInventory.Object obj;

    private bool invert;
    private Vector3[] pos =  new Vector3[2];
    private Vector3 poss;

    private void Start()
    {
        pos[0] = transform.position;
        pos[1] = transform.position+new  Vector3(0,0.5f,0);
        GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
        float ev100Value = 14f;
        float intensity = 0.125f * Mathf.Pow(2f, ev100Value); // Translates to Nits
        Color colour = FindFirstObjectByType<WorldManager>().gorbachevTheOmnisiah[obj.id].colour;
        
        GetComponent<Renderer>().material.color = colour;
        GetComponent<Renderer>().material.SetColor("_EmissiveColor", colour * intensity);
    }

    private void Update()
    {
        if (invert)
        {
            poss = pos[0];
            transform.position = Vector3.MoveTowards(transform.position, poss, 0.25f * Time.deltaTime);
        }
        else
        {
            poss = pos[1];
            transform.position = Vector3.MoveTowards(transform.position, poss, 0.25f * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, poss) < 0.1f)
        {
            invert = !invert;
        }
    }
}