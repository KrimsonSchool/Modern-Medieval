using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool mouseAvailable;

    public AudioClip clooornk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mouseAvailable)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OPEN_SCENE(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OPEN_SCENE_INDEX(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        FindFirstObjectByType<SoundBlaster98>().TriggerSound(clooornk);
    }

    public void RELOAD_SCENE()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindFirstObjectByType<SoundBlaster98>().TriggerSound(clooornk);
    }

    public void QUIT_GAME()
    {
        Application.Quit();
        FindFirstObjectByType<SoundBlaster98>().TriggerSound(clooornk);
    }

    public void ENABLE_OBJECT(GameObject obj)
    {
        obj.SetActive(true);
        FindFirstObjectByType<SoundBlaster98>().TriggerSound(clooornk);
    }
}
