using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.UI;
using TMPro;

public class AiAgent : MonoBehaviour
{
    public AiStateId initialState;
    public AiAgentConfig config;
    public TextMeshProUGUI state;

    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public EnemyHealthBar healthBar;
    [HideInInspector] public GameObject player;
    [HideInInspector] public AiSensor sensors;
    public float distance;
    [HideInInspector] public float startTime = 0.0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        player = GameObject.FindGameObjectWithTag("Player");
        sensors = GetComponent<AiSensor>();


        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiWanderState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
        state.text = "Current State: " + stateMachine.currentState;
        if (startTime < 5.0f) 
        { 
            startTime += Time.deltaTime; 
        }
        distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
    }
}
