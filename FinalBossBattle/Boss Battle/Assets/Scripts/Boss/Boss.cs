using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float maxHealth = 300;
    public float currentHealth;
    public float distance;
    public bool isAlive = true;
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.025f;
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
        currentHealth = maxHealth;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        weaponController = weaponHolder.GetComponent<WeaponController>();
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" && weaponController.isAttacking)
        {
            TakeDamage(10);
            Debug.Log("-10");
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(50);
        }
        distance = Vector3.Distance(navMesh.transform.position, player.transform.position);

        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
        }
    }

    private void Die()
    {
        isAlive = false;
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
