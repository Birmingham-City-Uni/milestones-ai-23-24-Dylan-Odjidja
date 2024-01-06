using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnMinions : ActionNode
{
    public GameObject boss;
    public Health health;
    public Animator animator;
    public NavMeshAgent navMesh;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        health = boss.GetComponent<Health>();
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (health.currentHealth == 150)
        {
            navMesh.isStopped = true;
            animator.SetTrigger("Scream");
            //animator.SetBool("isAwake", false);
            Debug.Log("Minion Mode");
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
