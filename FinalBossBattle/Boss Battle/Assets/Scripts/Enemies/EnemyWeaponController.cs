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
    [HideInInspector] public BoxCollider boxCollider;

    private void Start()
    {
        animator = sword.GetComponent<Animator>();
        boxCollider = sword.GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
        boxCollider.enabled = true;
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
        boxCollider.enabled = false;
    }
}
