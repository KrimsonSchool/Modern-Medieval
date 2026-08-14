using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public GameObject MM;

    private Canvas[] menus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menus = FindObjectsByType<Canvas>(FindObjectsSortMode.None);

        foreach (Canvas c in menus)
        {
            c.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        MM.SetActive(true);
        foreach (Canvas c in menus)
        {
            c.enabled = true;
        }
        gameObject.SetActive(false);
    }
}
