using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WakeUp : ActionNode
{
    public GameObject boss;
    public Boss bossScript;
    public Animator animator;
    public TextMeshProUGUI title;

    protected override void OnStart()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossScript = boss.GetComponent<Boss>();
        animator = boss.GetComponent<Animator>();
        title = boss.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (bossScript.Stage == 1)
        {
            animator.SetTrigger("Scream");
            title.text = "Stage 1: 10 dmg";
            animator.SetBool("isAwake", true);
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
        
    }
}
