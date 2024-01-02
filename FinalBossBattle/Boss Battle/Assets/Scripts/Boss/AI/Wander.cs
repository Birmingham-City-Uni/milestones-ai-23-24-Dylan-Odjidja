using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : ActionNode
{
    public GameObject boss;
    public Animator animator;
    public NavMeshAgent navMesh;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        navMesh.speed = 1.5f;
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if(!navMesh.hasPath)
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            Vector3 min = worldBounds.min.position;
            Vector3 max = worldBounds.max.position;

            Vector3 randomPosition = new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
                );
            navMesh.destination = randomPosition;
        }
        return State.Success;
    }
}
