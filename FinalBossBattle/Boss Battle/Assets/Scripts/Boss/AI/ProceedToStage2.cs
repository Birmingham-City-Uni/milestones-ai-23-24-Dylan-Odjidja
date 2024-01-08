using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ProceedToStage2 : ActionNode
{
    public GameObject boss;
    public Boss bossScript;
    public Health health;
    public Animator animator;
    public NavMeshAgent navMesh;
    public TextMeshProUGUI title;
    public GameObject gm;
    public GameManager gameManager;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossScript = boss.GetComponent<Boss>();
        health = boss.GetComponent<Health>();
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        title = boss.GetComponentInChildren<TextMeshProUGUI>();
        gm = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (health.currentHealth == 150 && bossScript.Stage == 1)
        {
            navMesh.isStopped = true;
            animator.SetTrigger("Scream");
            //title.text = "Defeat the Minions!";
            //animator.SetBool("isAwake", false);
            bossScript.Stage = 2;
            //if (gameManager.minionsDead == true)
            //{
                
            //}
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
        
    }
}
