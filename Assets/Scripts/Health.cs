using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int baseHealth;
    private int _health;

    public Slider healthBar;
    public bool kill = false;

    protected virtual void Start()
    {
        _health = baseHealth;
        healthBar.maxValue = baseHealth;
        healthBar.value = _health;
    }

    public virtual bool Hurt(int damage)
    {
        _health-=damage;
        healthBar.value = _health;

        if (_health <= 0)
        {
            kill = true;
            Death();
        }
        return kill;
    }

    public virtual void Death(){}
}
