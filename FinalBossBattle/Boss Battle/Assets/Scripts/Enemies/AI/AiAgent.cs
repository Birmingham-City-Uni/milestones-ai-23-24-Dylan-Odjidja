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

    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public GameObject player;
    [HideInInspector] public AiSensor sensors;
    [HideInInspector] public EnemyWeaponController enemyWeaponController;
    public float distance;
    [HideInInspector] public float startTime = 0.0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        sensors = GetComponent<AiSensor>();
        enemyWeaponController = GetComponentInChildren<EnemyWeaponController>();


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
        if (startTime < 5.0f) 
        { 
            startTime += Time.deltaTime; 
        }
        distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
    }
}
