using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Wander : ActionNode
{
    public GameObject boss;
    public Animator animator;
    public NavMeshAgent navMesh;
    public AiSensor sensor;
    public AttackSensor attackSensor;
    public Boss bossScript;
    public GameObject gm;
    public GameManager gameManager;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        sensor = boss.GetComponentInChildren<AiSensor>();
        attackSensor = boss.GetComponent<AttackSensor>();
        bossScript = boss.GetComponent<Boss>();
        gm = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
        animator.SetFloat("Speed", navMesh.velocity.magnitude);
        navMesh.isStopped = false;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if(!navMesh.hasPath && !gameManager.player)
        {
            navMesh.speed = 3.5f;

            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            Vector3 min = worldBounds.min.position;
            Vector3 max = worldBounds.max.position;

            Vector3 randomPosition = new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
                );
            navMesh.destination = randomPosition;

            return State.Success;
        }
        else 
        {
            return State.Failure; 
        }
        
    }
}
