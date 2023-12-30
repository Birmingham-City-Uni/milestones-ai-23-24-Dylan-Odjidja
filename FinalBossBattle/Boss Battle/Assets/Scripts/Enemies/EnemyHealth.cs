using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    [HideInInspector] public GameObject weaponHolder;
    [HideInInspector] public GameObject parent;
    [HideInInspector] public WeaponController weaponController;
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public bool isAlive = true;

    void Start()
    {
        currentHealth = maxHealth;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        weaponController = weaponHolder.GetComponent<WeaponController>();
        parent = GameObject.FindGameObjectWithTag("Parent");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" && weaponController.isAttacking)
        {
            TakeDamage(10);
            Debug.Log("-10");
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            TakeDamage(50);
        }
    }

    private void Die()
    {
        isAlive = false;
    }
}
