using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LayerManager : MonoBehaviour
{
    public int layer;
    //public TextMeshProUGUI layerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        OnSceneLoad(SceneManager.GetActiveScene(), LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //layerText = GameObject.Find("LayerText").GetComponent<TextMeshProUGUI>();
        DisplayNewLayer();

        
        /*
        int rand = Random.Range(0, 3);
        
        switch (rand)
        {
            case 0:
                //open floor pit
                FindFirstObjectByType<WorldManager>().differences[0].SetActive(false);
                break;
            
        }*/
    }
    
    public void DisplayNewLayer()
    {
        //layerText.text = "Layer " + layer.ToString("D2");
        //layerText.GetComponent<Animator>().Play("LayerText");
        
    }

    public void Reset()
    {
        layer = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
