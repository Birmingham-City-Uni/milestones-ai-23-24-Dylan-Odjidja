using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    [HideInInspector] EnemyWeaponController enemyWeaponController;

    void Start()
    {
        currentHealth = maxHealth;
        enemyWeaponController = GetComponent<EnemyWeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySword")
        {
            TakeDamage(5);
            Debug.Log("-5");
            //Instantiate(HitParticle.transform, 
            //    new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
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
        
    }
}
