using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject door;

    public Vector3 doorEndDiff;
    private Vector3 doorStartPos;

    private bool open;
    private bool opening;

    public float doorSpeed;

    private float timer;

    public GameObject[] layLines;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorStartPos = door.transform.position;
        
        //  OpenDoor();
    }

    // Update is called once per frame
    void Update()
    {
        if (opening && !open)
        {
            timer += Time.deltaTime*doorSpeed;
            if (door.transform.position != (doorStartPos+ doorEndDiff))
            {
                door.transform.position = Vector3.Lerp(doorStartPos, (doorStartPos+ doorEndDiff), timer);
            }
            else
            {
                open = true;
                opening = false;
            }
        }
    }

    public void OpenDoor()
    {
        opening = true;
        layLines[0].SetActive(false);
        layLines[1].SetActive(true);
    }
}
