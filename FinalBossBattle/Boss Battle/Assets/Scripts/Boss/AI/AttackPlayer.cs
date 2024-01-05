using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : ActionNode
{
    public GameObject boss;
    public Animator animator;
    public NavMeshAgent navMesh;
    public AiSensor sensor;
    public AttackSensor attackSensor;
    public Boss bossScript;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        sensor = boss.GetComponentInChildren<AiSensor>();
        attackSensor = boss.GetComponent<AttackSensor>();
        bossScript = boss.GetComponent<Boss>();
        navMesh.speed = 3.5f;
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (attackSensor.objects.Count > 0 && sensor.objects.Count > 0)
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
