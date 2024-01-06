using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : ActionNode
{
    public GameObject boss;
    public Animator animator;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        animator = boss.GetComponent<Animator>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        animator.SetTrigger("Scream");
        animator.SetBool("isAwake", true);
        return State.Success;
    }
}
