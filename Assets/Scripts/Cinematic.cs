using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public GameObject MM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        MM.SetActive(true);
        gameObject.SetActive(false);
    }
}
