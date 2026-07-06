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

    public bool mult;
    public DoorOpener multDad;
    public GameObject[] orbOfTheAncients;
    public bool[] unlocked;

    private int unlockedAm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (door!=null)
        {
            doorStartPos = door.transform.position;
        }

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
        if (requiredID != -1)
        {
            Color colour = FindFirstObjectByType<WorldManager>().gorbachevTheOmnisiah[requiredID].colour;


            GetComponent<Renderer>().material.color = colour;
            matt.SetColor("_EmissiveColor", colour * intensity);

            mats[materialIndex] = matt;

            renderer.materials = mats;
        }
    }
    void Update()
    {
        if (mult)
        {
            unlockedAm = 0;
            for (int i = 0; i < unlocked.Length; i++)
            {
                if (unlocked[i])
                {
                    unlockedAm++;

                    if (orbOfTheAncients[i] != null)
                    {
                        SetColour(orbOfTheAncients[i].GetComponent<MeshRenderer>(), i);
                    }
                }
            }

            if (unlockedAm >= unlocked.Length)
            {
                print("opening door, " + timer);
                OpenDoor();
            }
        }

        if (opening && !open && door!=null)
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

        if (opening && door != null)
        {
            door.transform.position = doorStartPos + doorEndDiff;
            open = true;
            opening = false;
        }

        if (!open)
        {
            if (inventory.HasItemWithID(requiredID))
            {
                //inventory.RemoveItemIndex(inventory.FindItemIdIndex(requiredID));
                inventory.RemoveItem(requiredID);
                if (mult)
                {
                    multDad.unlocked[requiredID] = true;
                }
                else
                {
                    OpenDoor();
                }
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

    public void SetColour(MeshRenderer ren, int index)
    {

        Material[] mats = ren.materials;

        Material matt = new Material(mats[0]);
        float ev100Value = 14;
        float intensity = 0.125f * Mathf.Pow(2f, ev100Value); // Translates to Nits
        
        Color colour = FindFirstObjectByType<WorldManager>().gorbachevTheOmnisiah[index].colour;


        GetComponent<Renderer>().material.color = colour;
        matt.SetColor("_EmissiveColor", colour * intensity);

        mats[0] = matt;

        ren.materials = mats;
    }
}