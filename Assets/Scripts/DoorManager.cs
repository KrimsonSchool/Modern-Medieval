using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class DoorManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private MenuManager menu;
    
    private bool vidPlaying;
    public GameObject layerManager;

    private string _nextLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = FindFirstObjectByType<MenuManager>();

        if (FindFirstObjectByType<LayerManager>() == null)
        {
            Instantiate(layerManager);
        }
        
        videoPlayer.targetCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidPlaying && !videoPlayer.isPlaying)
        {
            FindFirstObjectByType<LayerManager>().layer++;
            menu.OPEN_SCENE(_nextLevel);
        }
    }

    public IEnumerator PlayVid(string nextLevel)
    {
        Canvas[] menus = FindObjectsByType<Canvas>(FindObjectsSortMode.None);

        foreach (Canvas c in menus)
        {
            c.enabled = false;
        }
        
        _nextLevel = nextLevel;
        videoPlayer.Play();
        yield return new WaitForSeconds(0.1f);
        vidPlaying=true;
    }
}
