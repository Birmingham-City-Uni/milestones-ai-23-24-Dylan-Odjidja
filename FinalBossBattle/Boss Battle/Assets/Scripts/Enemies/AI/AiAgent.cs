using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public EnemyHealthBar healthBar;
    public GameObject player;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine = new AiStateMachine(this);
        stateMachine.RegsiterState(new AiChasePlayerState());
        stateMachine.RegsiterState(new AiDeathState());
        stateMachine.RegsiterState(new AiIdleState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
    }
}
