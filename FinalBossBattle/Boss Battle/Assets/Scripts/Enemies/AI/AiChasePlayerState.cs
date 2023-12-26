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
        
    }

    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }

        if (agent.sensors.objects.Count > 0) 
        {
            foreach (var obj in agent.sensors.objects)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = obj.transform.position;
                    if (agent.distance <= agent.config.minDistance)
                    {
                        agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
                    }
                }
            }
        }
        else
        {
            agent.stateMachine.ChangeState(AiStateId.Wander);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
