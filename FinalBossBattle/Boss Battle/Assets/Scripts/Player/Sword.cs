using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private WeaponController weaponController;

    private void Start()
    {
        weaponController = GetComponentInParent<WeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && weaponController.isAttacking)
        {
            Health enemyHealth = other.GetComponent<Health>();
            enemyHealth.TakeDamage(10);
        }
        else if (other.tag == "Boss" && weaponController.isAttacking)
        {
            Health bossHealth = other.GetComponent<Health>();
            bossHealth.TakeDamage(10);
        }
    }
}
