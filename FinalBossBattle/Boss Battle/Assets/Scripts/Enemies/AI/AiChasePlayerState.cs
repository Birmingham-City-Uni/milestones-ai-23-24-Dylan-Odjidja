using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 3.5f;
    }

    public void Update(AiAgent agent)
    {
        if (agent.gameManager.player != null)
        {
            if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
            {
                if (agent.distance < agent.config.minDistance)
                {
                    agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
                }
                else
                {
                    agent.navMeshAgent.isStopped = false;
                    agent.navMeshAgent.destination = agent.gameManager.player.transform.position;
                }
            }
        }

        if (agent.enemyHealth.isAlive == false)
        {
            agent.stateMachine.ChangeState(AiStateId.Death);
        }
    }

    public void Exit(AiAgent agent)
    {

    }
}
