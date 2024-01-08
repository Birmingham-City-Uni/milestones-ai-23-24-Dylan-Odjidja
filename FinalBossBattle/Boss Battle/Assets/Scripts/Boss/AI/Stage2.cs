using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Stage2 : ActionNode
{
    public GameObject boss;
    public Boss bossScript;
    public Attacks attacks;
    public Animator animator;
    public NavMeshAgent navMesh;
    public TextMeshProUGUI title;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossScript = boss.GetComponent<Boss>();
        attacks = boss.GetComponentInChildren<Attacks>();
        animator = boss.GetComponent<Animator>();
        navMesh = boss.GetComponent<NavMeshAgent>();
        title = boss.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (bossScript.Stage == 2)
        {
            navMesh.isStopped = false;
            //animator.SetTrigger("Scream");
            title.text = "Stage 2: 20 dmg";
            animator.SetBool("isAwake", true);
            attacks.damage = 20;
            return State.Failure;
        }
        else
        {
            return State.Success;
        }
    }
}
