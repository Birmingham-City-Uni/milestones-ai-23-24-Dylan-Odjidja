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
            TakeDamage(20);
            Debug.Log("-20");
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
        Destroy(gameObject);
    }
}
