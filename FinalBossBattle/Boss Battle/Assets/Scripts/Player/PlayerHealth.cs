using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    [HideInInspector] public bool isAlive = true;
    [HideInInspector] EnemyWeaponController enemyWeaponController;

    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
        enemyWeaponController = GetComponent<EnemyWeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySword")
        {
            TakeDamage(5);
        }
        else if (other.tag == "Boss Head")
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        isAlive = false;
    }
}
