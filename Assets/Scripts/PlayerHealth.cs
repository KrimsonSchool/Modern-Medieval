using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public GameObject hurtEffect;
    public float hurtEffectTime;
    float hurtEffectTimer;
    PlayerHolder playerHolder;
    
    Vector3 startPos;
    
    WorldManager worldManager;
    
    [Space] SoundBlaster98 sound;
    
    protected override void Start()
    {
        sound = FindFirstObjectByType<SoundBlaster98>();
        worldManager = FindFirstObjectByType<WorldManager>();
        
        startPos = transform.position;
        base.Start();
    }
    
    void Update()
    {
        if (healthBar == null)
        {
            healthBar = playerHolder.healthBar;
        }
        
        if (hurtEffect != null)
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
        else
        {
            hurtEffect = playerHolder.hurtEffect;
        }

        if (HP != baseHealth)
        {
            if (!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
            }
        }
        else
        {
            if (healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(false);
            }
        }
    }

    public override bool Hurt(int damage)
    {
        sound.TriggerSound(worldManager.sounds[2]);
        
        base.Hurt(damage);
        hurtEffect.SetActive(true);
        return kill;
    }

    public override void Death()
    {
        FindFirstObjectByType<LayerManager>().Reset();
        base.Reset();
    }

    public void ResetPos()
    {
        playerHolder= GetComponent<PlayerHolder>();
        hurtEffect = playerHolder.hurtEffect;
        healthBar = playerHolder.healthBar;
        transform.position = startPos;
        SetHealthBar();
    }
}
