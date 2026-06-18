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

    [Space] public MeshRenderer meshToSetMaterial;
    public int materialIndex;

    [Space] public string typeName;
    //public GameObject[] possiblePuzzleSpawns;

    //public PuzzleType puzzleType;

    public int requiredID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorStartPos = door.transform.position;

        foreach (var lay in layLines)
        {
            lay.AddComponent<FaeQue>();
        }
        /*if (possiblePuzzleSpawns.Length > 0)
        {
            possiblePuzzleSpawns[Random.Range(0, possiblePuzzleSpawns.Length)].SetActive(true);
        }*/
        //  OpenDoor();

        MeshRenderer renderer = meshToSetMaterial.GetComponent<MeshRenderer>();

        Material[] mats = renderer.materials;

        Material matt = new Material(mats[materialIndex]);
        float ev100Value = 14;
        float intensity = 0.125f * Mathf.Pow(2f, ev100Value); // Translates to Nits
        Color colour = FindFirstObjectByType<WorldManager>().gorbachevTheOmnisiah[requiredID];

        matt.SetColor("_EmissiveColor", colour * intensity);

        mats[materialIndex] = matt;

        renderer.materials = mats;
    }

    // Update is called once per frame
    void Update()
    {
        if (opening && !open)
        {
            timer += Time.deltaTime * doorSpeed;
            if (door.transform.position != (doorStartPos + doorEndDiff))
            {
                door.transform.position = Vector3.Lerp(doorStartPos, (doorStartPos + doorEndDiff), timer);
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

        /*TryGetComponent<Collider>(out var col);

        if (col != null)
        {
            col.enabled = false;
        }*/
    }

    public void TryOpenDoor()
    {
        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();
        if (!open)
        {
            if (inventory.HasItemWithID(requiredID))
            {
                //inventory.RemoveItemIndex(inventory.FindItemIdIndex(requiredID));
                inventory.RemoveItem(requiredID);

                OpenDoor();
            }
        }
        else
        {
            PlayerInventory.Object key = new PlayerInventory.Object
            {
                id = requiredID,
                name = typeName
            };
            inventory.AddItem(key);
            open = false;
            opening = false;
            if (layLines.Length > 0)
            {
                layLines[0].SetActive(true);
                layLines[1].SetActive(false);
            }

            door.transform.position = doorStartPos;
            timer = 0;
        }
    }
}