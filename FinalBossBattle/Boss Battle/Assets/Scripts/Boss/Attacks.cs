using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    private Boss boss;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Health playerHealth = other.GetComponent<Health>();
            playerHealth.TakeDamage(20);
        }
    }
}