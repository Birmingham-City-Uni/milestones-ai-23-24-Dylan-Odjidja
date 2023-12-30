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
        if (agent.distance <= agent.config.minDistance)
        {
            if (agent.enemyWeaponController.CanAttack == true && !agent.enemyWeaponController.isAttacking)
            {
                agent.enemyWeaponController.SwordAttack();
            }
        }
        else if (agent.distance > agent.config.minDistance && agent.sensors.objects.Count > 0)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
        else
        {
            agent.stateMachine.ChangeState(AiStateId.Wander);
        }

        if (agent.enemyHealth.isAlive == false)
        {
            agent.stateMachine.ChangeState(AiStateId.Death);
        }
    }
}
