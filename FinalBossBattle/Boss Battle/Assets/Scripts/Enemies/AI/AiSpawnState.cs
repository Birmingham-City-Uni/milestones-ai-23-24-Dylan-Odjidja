using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawnState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Spawn;
    }

    public void Enter(AiAgent agent)
    {
        agent.SpawnParticles();
        agent.capsule.SetActive(true);
        agent.capsuleCollider.enabled = true;
    }

    public void Update(AiAgent agent)
    {
        if (agent.startTime > 3f && agent.gameManager.player != null)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
