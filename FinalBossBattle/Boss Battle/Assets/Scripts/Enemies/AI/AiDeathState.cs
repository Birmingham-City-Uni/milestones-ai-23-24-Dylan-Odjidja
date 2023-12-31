using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiDeathState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Death;
    }

    public void Enter(AiAgent agent)
    {
        agent.SpawnHealthItem();
        agent.gameManager.minions -= 1;
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        //agent.animator.SetBool("IsAlive", false);
    }
}
