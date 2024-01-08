using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Health health = other.GetComponent<Health>();
            if (health.currentHealth < 100)
            {
                health.Heal(10);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0.5f, 0 * Time.deltaTime);
    }
}
