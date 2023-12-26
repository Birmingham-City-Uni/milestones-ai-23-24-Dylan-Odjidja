using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Update(AiAgent agent)
    {
        if (agent.startTime >= 5.0f)
        {
            agent.stateMachine.ChangeState(AiStateId.Wander);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
