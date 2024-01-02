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

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        sensor = boss.GetComponent<AiSensor>();
        navMesh.speed = 3.5f;
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (sensor.objects.Count > 0)
        {
            foreach (var obj in sensor.objects)
            {
                if (navMesh.pathStatus != NavMeshPathStatus.PathPartial)
                {
                        navMesh.destination = obj.transform.position;
                }
            }
        }
        return State.Success;
    }
}
