using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    GameObject[] objects;

    private int rng;
    
    WorldManager worldManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worldManager = FindFirstObjectByType<WorldManager>();
        objects = worldManager.worldObjects;
        rng = Random.Range(0, objects.Length);
        Instantiate(objects[rng], transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
