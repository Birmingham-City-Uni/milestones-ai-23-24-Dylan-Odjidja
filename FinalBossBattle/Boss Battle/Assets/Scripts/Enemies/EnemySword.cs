using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private EnemyWeaponController weaponController;
    public GameObject hitParticle;

    private void Start()
    {
        weaponController = GetComponentInParent<EnemyWeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && weaponController.isAttacking)
        {
            Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            Health enemyHealth = other.GetComponent<Health>();
            enemyHealth.TakeDamage(5);
        }
    }
}
