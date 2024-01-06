using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    Health bossHealth;
    private float lerpSpeed = 0.025f;

    void Start()
    {
        bossHealth = GetComponentInParent<Health>();
    }

    void Update()
    {
        if (healthSlider.value != bossHealth.currentHealth)
        {
            healthSlider.value = bossHealth.currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, bossHealth.currentHealth, lerpSpeed);
        }
    }
}
