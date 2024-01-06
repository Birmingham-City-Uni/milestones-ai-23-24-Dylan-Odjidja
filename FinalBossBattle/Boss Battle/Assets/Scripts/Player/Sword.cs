using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private WeaponController weaponController;
    public GameObject hitParticle;

    private void Start()
    {
        weaponController = GetComponentInParent<WeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && weaponController.isAttacking)
        {
            Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            Health enemyHealth = other.GetComponent<Health>();
            enemyHealth.TakeDamage(10);
        }
        else if (other.tag == "Boss" && weaponController.isAttacking)
        {
            Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            Health bossHealth = other.GetComponent<Health>();
            bossHealth.TakeDamage(10);
        }
    }
}
