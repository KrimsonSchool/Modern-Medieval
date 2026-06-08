using UnityEngine;

public class RandomEnabler : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int rng =  Random.Range(0, objects.Length);
        objects[rng].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
