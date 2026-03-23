using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHolder : MonoBehaviour
{
    public Slider healthBar;
    public Slider xpBar;
    public Slider cooldown;
    public GameObject hurtEffect;
    public WorldManager worldManager;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void Awake()
    {
        OnSceneLoad(SceneManager.GetActiveScene(), LoadSceneMode.Additive);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        worldManager = FindFirstObjectByType<WorldManager>();
        hurtEffect = worldManager.hurtEffect;
        healthBar = worldManager.healthSlider.GetComponent<Slider>();
        cooldown = worldManager.cooldownSlider.GetComponent<Slider>();  
        xpBar = worldManager.xpSlider.GetComponent<Slider>();
        
        GetComponent<PlayerHealth>().ResetPos();
    }
}