using System;
using InputSystemGlobal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapons : MonoBehaviour
{
    private InputSystem_Actions controls;
    private void Awake() => controls = new InputSystem_Actions();
    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    private bool attack;
    public Animator weaponAnimator;

    public float hitDist;

    public int damage;

    [Tooltip("Smaller number = faster")]
    public float attackSpeed;
    private bool hasAttacked;

    private float attackTimer;

    public Slider abilityCooldown;
    public Slider xpBar;
    
    PlayerHolder playerHolder;

    private float xpMax=5;
    private int xp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHolder = GetComponent<PlayerHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        attack = controls.Player.Attack.triggered;

        if (abilityCooldown == null)
        {        
            abilityCooldown = playerHolder.cooldown;
            abilityCooldown.maxValue = attackSpeed;
        }

        if (xpBar == null)
        {
            xpBar = playerHolder.xpBar;
            xpBar.maxValue = xpMax;
        }

        if (hasAttacked)
        {
            abilityCooldown.value = attackTimer;
            attackTimer+=Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                hasAttacked = false;
            }
        }
        
        if (attack && !hasAttacked)
        {
            hasAttacked = true;
            attackTimer = 0;
            weaponAnimator.Play("WeaponAttack");
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, hitDist))
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.CompareTag("Enemy"))
                {
                    if (hitObj.GetComponent<Health>().Hurt(damage))
                    {
                        xp++;
                        if (xp> Mathf.RoundToInt(xpMax))
                        {
                            xp = 0;
                            xpMax *= 1.1f;
                            xpBar.maxValue = xpMax;
                        }
                        xpBar.value = xp;
                    }
                }
            }
        }
    }
}
