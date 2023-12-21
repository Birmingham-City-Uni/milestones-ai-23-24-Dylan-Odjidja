using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateId initialState;
    public AiAgentConfig config;

    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public EnemyHealthBar healthBar;
    [HideInInspector] public GameObject player;
    [HideInInspector] public AiSensor sensors;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        player = GameObject.FindGameObjectWithTag("Player");
        sensors = GetComponent<AiSensor>();


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
