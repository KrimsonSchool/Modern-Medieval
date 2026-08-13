using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PDA : MonoBehaviour
{
    public GameObject pda;


    public TextMeshProUGUI title; // gaol
    public TextMeshProUGUI stack; // inventory
    public RawImage imgStack;

    //[HideInInspector]
    //public int selected;

    //[HideInInspector]
    //public bool stackLock;

    [HideInInspector] public bool up = true;

    public Vector3 pdaPosOrigin;
    public Vector3 pdaPosDown;

    [HideInInspector] public int mapNo;
    [HideInInspector] public Texture map;

    private Transform playerTransform;
    public RectTransform mapRect;
    public RectTransform playerIcon;

    public Vector2[] worldBottomLeft;

    public Vector2[] worldTopRight;

    //needs to contain: Inventory -> keys, Map, 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pda.SetActive(true);
        up = true;

        pdaPosOrigin = pda.transform.localPosition;
        pdaPosDown = pda.transform.localPosition + Vector3.down * 0.5f;

        worldBottomLeft = new Vector2[4];
        worldTopRight = new Vector2[4];

        worldBottomLeft[0] = new Vector2(7.463f, -70.036f);
        worldTopRight[0] = new Vector2(37.17892f, -29.90228f);

        worldBottomLeft[1] = new Vector2(17.8352f, 22.1441f);
        worldTopRight[1] = new Vector2(-12.09684f, -18.26811f);

        worldBottomLeft[2] = new Vector2(35.1913f, -14.23739f);
        worldTopRight[2] = new Vector2(76.6822357f, 18.1978474f);

        imgStack.texture = map;
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = transform;
        
        UpdateWorldSpaceMinimap();

        if (up)
        {
            if (pda.transform.localPosition != pdaPosOrigin)
            {
                pda.transform.localPosition = Vector3.MoveTowards(pda.transform.localPosition, pdaPosOrigin, Time.deltaTime);
                //pda.transform.localPosition = pdaPosOrigin;
            }
        }
        else
        {
            if (pda.transform.localPosition != pdaPosDown)
            {
                pda.transform.localPosition = Vector3.MoveTowards(pda.transform.localPosition, pdaPosDown, Time.deltaTime);
            }
        }
    }

    private void UpdateWorldSpaceMinimap()
    {
        if (playerTransform == null || mapRect == null || playerIcon == null) return;

        // 1. Get the player's current X and Z in the 3D world
        float playerX = playerTransform.position.x;
        float playerZ = playerTransform.position.z;

        // 2. Normalize the position (Returns a value between 0.0 and 1.0)
        float normalizedX = Mathf.InverseLerp(worldBottomLeft[mapNo].x, worldTopRight[mapNo].x, playerX);
        float normalizedY = Mathf.InverseLerp(worldBottomLeft[mapNo].y, worldTopRight[mapNo].y, playerZ);

        // 3. Calculate local position based on the map's actual Rect size
        // We subtract 0.5f so the center is (0,0) locally
        float localX = (normalizedX - 0.5f) * mapRect.rect.width;
        float localY = (normalizedY - 0.5f) * mapRect.rect.height;

        // 4. Apply as localPosition
        // We set Z to a slightly negative value (like -1) so the icon hovers just in front of the map canvas to prevent Z-fighting
        playerIcon.localPosition = new Vector3(localX, localY, -1f);
    }
}