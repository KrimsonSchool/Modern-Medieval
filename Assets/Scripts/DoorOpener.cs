using UnityEngine;

public enum PuzzleType
{
    Fetch,
    FloorButton,
    Lever
}

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
    public GameObject[] possiblePuzzleSpawns;
    
    public PuzzleType puzzleType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorStartPos = door.transform.position;
        if (possiblePuzzleSpawns.Length > 0)
        {
            possiblePuzzleSpawns[Random.Range(0, possiblePuzzleSpawns.Length)].SetActive(true);
        }
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
        if (layLines.Length > 0)
        {
            layLines[0].SetActive(false);
            layLines[1].SetActive(true);
        }

        TryGetComponent<Collider>(out var col);

        if (col != null)
        {
            col.enabled = false;
        }
    }
}
