using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }

    public void RELOAD_SCENE()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QUIT_GAME()
    {
        Application.Quit();
    }

    public void ENABLE_OBJECT(GameObject obj)
    {
        obj.SetActive(true);
    }
}
