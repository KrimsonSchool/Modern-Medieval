using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public struct Object
    {
        public int id;
        public string name;
        public GameObject prefab;
    }
    

    public List<Object> inventory;

    private TextMeshProUGUI keysText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keysText = GameObject.Find("test_keys").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        keysText.text = "";
        foreach (var obj in inventory)
        {
            keysText.text += obj.name + " [" + obj.id+"]\n";
        }
    }

    public void AddItem(Object item)
    {
        inventory.Add(item);
    }

    public void RemoveItemIndex(int index)
    {
        inventory.RemoveAt(index);
    }
    
    public void RemoveItem(int id)
    {
        inventory.Remove(inventory[inventory.FindIndex(x => x.id == id)]);
    }

    public bool HasItemWithID(int id)
    {
        foreach (var obj in inventory)
        {
            if (obj.id == id)
            {
                return true;
            }
        }
        return false;
    }

    public int FindItemIdIndex(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }
}