using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    PlayerHealth playerHealth;
    private float lerpSpeed = 0.025f;

    void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        playerHealth.currentHealth = playerHealth.maxHealth;
    }

    void Update()
    {
        if (healthSlider.value != playerHealth.currentHealth)
        {
            healthSlider.value = playerHealth.currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerHealth.currentHealth, lerpSpeed);
        }

        if (Input.GetKeyDown(KeyCode.J)) 
        {
            playerHealth.TakeDamage(5);
        }
    }
}
