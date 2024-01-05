using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private EnemyWeaponController weaponController;

    private void Start()
    {
        weaponController = GetComponentInParent<EnemyWeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && weaponController.isAttacking)
        {
            Health enemyHealth = other.GetComponent<Health>();
            enemyHealth.TakeDamage(5);
        }
    }
}
