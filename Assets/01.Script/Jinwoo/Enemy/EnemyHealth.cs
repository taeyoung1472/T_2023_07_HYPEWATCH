using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool isDie = false;

    void Start()
    {
        currentHealth = maxHealth;    
    }

    private void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
