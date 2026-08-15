using UnityEngine;

public class Pity : MonoBehaviour
{
    [HideInInspector]
    public int fallDmg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        fallDmg = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDamage()
    {
        fallDmg += 3;
    }
}
