using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
    float timer = 0.0f;

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

        timer -= Time.deltaTime;


        if (timer < 0.0f)
        {
            Vector3 direction = (agent.player.transform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            //if (direction.sqrMagnitude > agent.config.minDistance * agent.config.minDistance)
            //{

            //}
            foreach (var obj in agent.sensors.objects)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = obj.transform.position;
                }
            }

            timer = agent.config.maxTime;
        }
        else if (!agent.navMeshAgent.hasPath)
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
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
