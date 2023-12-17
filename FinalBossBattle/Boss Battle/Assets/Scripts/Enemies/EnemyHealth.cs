using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public GameObject weaponHolder;
    public WeaponController weaponController;
    public MeshRenderer meshRenderer;
    EnemyHealthBar healthBar;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    void Start()
    {
        currentHealth = maxHealth;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        meshRenderer = GetComponent<MeshRenderer>();
        weaponController = weaponHolder.GetComponent<WeaponController>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" && weaponController.isAttacking)
        {
            TakeDamage(10);
            Debug.Log("-10");
            //Instantiate(HitParticle.transform, 
            //    new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }

        blinkTimer = blinkDuration;
    }

    private void Die()
    {
        Destroy(gameObject);
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intesity = (lerp * blinkIntensity) + 0.1f;
        meshRenderer.material.color = Color.white * intesity;
    }
}
