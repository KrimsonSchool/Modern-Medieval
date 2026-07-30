using UnityEngine;

public class PenroseOroboris : MonoBehaviour
{
    public GameObject shomBlit;
    
    Vector3 startPos;
    bool activated;

    public DoorOpener papa;
    public GameObject boom;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != startPos && !activated)
        {
            Activate();
        }
    }
    
    public void Activate()
    {
        shomBlit.SetActive(true);
        FindFirstObjectByType<Boss>().Hurt();
        
        transform.position = startPos;
        
        papa.gameObject.tag = "Untagged";
        papa.enabled = false;
        boom.SetActive(true);
        
        activated = true;
    }
}
