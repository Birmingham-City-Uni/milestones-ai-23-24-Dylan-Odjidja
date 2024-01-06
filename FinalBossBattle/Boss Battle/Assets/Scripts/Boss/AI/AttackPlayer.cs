using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : ActionNode
{
    public GameObject boss;
    public Animator animator;
    public NavMeshAgent navMesh;
    public AttackSensor attackSensor;
    public Boss bossScript;
    public GameObject gm;
    public GameManager gameManager;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        attackSensor = boss.GetComponent<AttackSensor>();
        bossScript = boss.GetComponent<Boss>();
        gm = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
        navMesh.speed = 3.5f;
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (gameManager.player != null && attackSensor.objects.Count > 0)
        {
            navMesh.isStopped = true;
            bossScript.Attack();
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
