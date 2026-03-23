using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string code;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lock"))
        {
            other.GetComponent<DoorOpener>().OpenDoor();
            this.tag = "Untagged";
            FindFirstObjectByType<PlayerMovement>().heldObject = null;
            Destroy(this);
        }
    }
}
