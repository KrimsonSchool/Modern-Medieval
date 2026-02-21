using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int baseHealth;
    private int health;
    
    public GameObject hurtEffect;
    public float hurtEffectTime;
    float hurtEffectTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (hurtEffect.activeSelf)
        {
            hurtEffectTimer += Time.deltaTime;
            if (hurtEffectTimer >= hurtEffectTime)
            {
                hurtEffect.SetActive(false);
                hurtEffectTimer = 0;
            }
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        print("player has: "+health);
        hurtEffect.SetActive(true);
    }
}
