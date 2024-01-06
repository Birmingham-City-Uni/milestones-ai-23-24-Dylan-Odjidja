using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject sword;
    public bool CanAttack = true;
    public float AttackCooldown = 2f;
    public Animator animator;
    public bool isAttacking = false;

    private void Start()
    {
        animator = sword.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
        animator.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.45f);
        isAttacking = false;
    }
}
