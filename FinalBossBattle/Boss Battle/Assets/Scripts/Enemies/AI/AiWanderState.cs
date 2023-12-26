using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWanderState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Wander;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public void Update(AiAgent agent)
    {
        if (!agent.navMeshAgent.hasPath)
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            Vector3 min = worldBounds.min.position;
            Vector3 max = worldBounds.max.position;

            Vector3 randomPosition = new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
                );
            agent.navMeshAgent.destination = randomPosition;
        }
        else if (agent.sensors.objects.Count > 0)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}
