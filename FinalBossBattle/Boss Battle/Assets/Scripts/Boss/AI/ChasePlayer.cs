using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : ActionNode
{
    public GameObject boss;
    public Animator animator;
    public NavMeshAgent navMesh;
    public AiSensor sensor;
    public AttackSensor attackSensor;
    public GameObject gm;
    public Health health;
    public GameManager gameManager;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        sensor = boss.GetComponentInChildren<AiSensor>();
        attackSensor = boss.GetComponent<AttackSensor>();
        gm = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
        health = boss.GetComponent<Health>();
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
        navMesh.isStopped = false;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (gameManager.player != null && attackSensor.objects.Count <= 0 && health.currentHealth != 150)
        {
            navMesh.speed = 5;

            if (navMesh.pathStatus != NavMeshPathStatus.PathPartial)
            {
                navMesh.destination = gameManager.player.transform.position;
            }

            return State.Success;
        }
        else
        {
            return State.Failure;
        }
        
    }
}
