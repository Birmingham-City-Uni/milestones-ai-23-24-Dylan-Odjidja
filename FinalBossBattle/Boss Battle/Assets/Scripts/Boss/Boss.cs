using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float distance;
    public bool CanAttack = true;
    public float AttackCooldown = 1f;
    public Animator animator;
    public bool isAttacking = false;
    [HideInInspector] public GameObject weaponHolder;
    [HideInInspector] public WeaponController weaponController;
    [HideInInspector] public NavMeshAgent navMesh;
    [HideInInspector] public GameObject player;

    void Start()
    {
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        weaponController = weaponHolder.GetComponent<WeaponController>();
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        distance = Vector3.Distance(navMesh.transform.position, player.transform.position);
    }

    public void StopEnemy()
    {
        navMesh.isStopped = true;
    }

    public void Attack()
    {
        isAttacking = true;
        CanAttack = false;
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackIndex", Random.Range(0, 3));
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
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
