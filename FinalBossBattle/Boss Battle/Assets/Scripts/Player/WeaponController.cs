using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject sword;
    public bool CanAttack = true;
    public float AttackCooldown = 0.5f;
    public Animator animator;
    public bool isAttacking = false;

    private void Start()
    {
        sword = GameObject.FindGameObjectWithTag("Sword");
        animator = sword.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }
        }
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
