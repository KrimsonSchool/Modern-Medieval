using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool pda;
    public bool sword;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupItem()
    {
        print("PickupItem");
        if (pda)
        {
            FindFirstObjectByType<PDA>().enabled = true;
        }
        if (sword)
        {
            FindFirstObjectByType<PlayerWeapons>().enabled = true;
        }
        
        Destroy(gameObject);
    }
}
