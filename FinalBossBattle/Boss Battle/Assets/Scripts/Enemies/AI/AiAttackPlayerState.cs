using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackPlayerState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public void Update(AiAgent agent)
    {
        if (agent.sensors.objects.Count > 0)
        {
            foreach (var obj in agent.sensors.objects)
            {
                    agent.navMeshAgent.destination = obj.transform.position;
                    Debug.Log("Attacking");
            }
        }
        else
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}
