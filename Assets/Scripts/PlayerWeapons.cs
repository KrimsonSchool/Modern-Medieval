using System;
using InputSystemGlobal;
using UnityEngine;

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

    public float attackSpeed;
    private bool hasAttacked;

    private float attackTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attack = controls.Player.Attack.triggered;
        

        if (hasAttacked)
        {
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
                if (hitObj.tag == "Enemy")
                {
                    hitObj.GetComponent<Enemy>().Hurt(damage);
                }
            }
        }
    }
}
