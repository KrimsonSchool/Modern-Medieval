using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int baseHealth;
    private int _health;

    public Slider healthBar;
    public bool kill = false;
    
    protected virtual void Start()
    {
        //SetHealthBar();
        _health = baseHealth;
        SetHealthBar();
    }
    

    public virtual bool Hurt(int damage)
    {
        if (healthBar == null)
        {
            print("healthBar is null");
        }

        _health -= damage;
        healthBar.value = _health;


        if (_health <= 0)
        {
            kill = true;
            Death();
        }

        return kill;
    }

    public virtual void Death()
    {
    }

    public virtual void SetHealthBar()
    {
        healthBar.maxValue = baseHealth;
        healthBar.value = _health;
    }

    public virtual void Reset()
    {
        _health = baseHealth;
    }
}