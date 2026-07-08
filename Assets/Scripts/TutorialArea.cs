using System;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    public GameObject[] toKill;
    public GameObject[] toSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Entered()
    {
        foreach (var l in toKill)
        {
            l.SetActive(false);
        }

        foreach (var l in toSpawn)
        {
            l.SetActive(true);
        }
    }
}
