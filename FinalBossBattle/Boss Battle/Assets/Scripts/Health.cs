using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public bool isAlive = true;

    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth == 0f)
        {
            StartCoroutine(Die());
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
    }

    private IEnumerator Die()
    {
        isAlive = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
