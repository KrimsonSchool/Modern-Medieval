using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public GameObject hurtEffect;
    public float hurtEffectTime;
    float hurtEffectTimer;
    PlayerHolder playerHolder;

    protected override void Start()
    {
        playerHolder= GetComponent<PlayerHolder>();
        hurtEffect = playerHolder.hurtEffect;
        healthBar = playerHolder.healthBar;
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
    }

    public override bool Hurt(int damage)
    {
        base.Hurt(damage);
        hurtEffect.SetActive(true);
        return kill;
    }

    public override void Death()
    {
        FindFirstObjectByType<LayerManager>().Reset();
    }

    public void ResetPos()
    {
        transform.position = new Vector3(0, 1, 0f);
    }
}
