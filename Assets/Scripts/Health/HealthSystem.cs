using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IHeal
{
    public event Action<float> OnHealthChanged = delegate { };
    public event Action OnDead = delegate { };

    [SerializeField] private float health = 5;
    [SerializeField] private float maxHealth = 5;
    private bool isDead = false;

    public HealthSystem(int health)
    {
        maxHealth = health;
        this.health = health;
    }

    public float GetHealth() { return health; }

    public float GetHealthPercent() { return health / maxHealth; }

    public void Heal(float value)
    {
        health += value;
        if (health > maxHealth) health = maxHealth;

        OnHealthChanged?.Invoke(GetHealthPercent());
    }

    public void Damage(float value)
    {
        if (isDead)
            return;

        health -= value;
        if (health <= 0)
        {
            health = 0;
            isDead = true;
            OnDead();
        }
        OnHealthChanged?.Invoke(GetHealthPercent());
    }

    public void ResetData()
    {
        isDead = false;
        health = maxHealth;
        OnHealthChanged?.Invoke(GetHealthPercent());
    }
}
