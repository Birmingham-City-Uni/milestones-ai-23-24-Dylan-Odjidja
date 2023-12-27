using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    EnemyHealth enemyHealth;
    private float lerpSpeed = 0.025f;

    void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemyHealth.currentHealth = enemyHealth.maxHealth;
    }

    void Update()
    {
        if (healthSlider.value != enemyHealth.currentHealth)
        {
            healthSlider.value = enemyHealth.currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, enemyHealth.currentHealth, lerpSpeed);
        }
    }
}
