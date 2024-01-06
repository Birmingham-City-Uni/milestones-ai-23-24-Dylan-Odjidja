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
    public float distance;

    [SerializeField] private ParticleSystem spawnPartricles;
    private ParticleSystem spawnParticlesInstance;

    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    //[HideInInspector] public Animator animator;
    [HideInInspector] public GameObject player;
    [HideInInspector] public AiSensor sensors;
    [HideInInspector] public EnemyWeaponController enemyWeaponController;
    [HideInInspector] public Health enemyHealth;
    [HideInInspector] public TextMeshProUGUI text;
    [HideInInspector] public float startTime = 0.0f;
    [HideInInspector] public GameObject gm;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public GameObject capsule;
    [HideInInspector] public CapsuleCollider capsuleCollider;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        sensors = GetComponent<AiSensor>();
        enemyWeaponController = GetComponentInChildren<EnemyWeaponController>();
        enemyHealth = GetComponent<Health>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        //animator = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
        capsule = GameObject.FindGameObjectWithTag("Capsule");
        capsuleCollider = GetComponent<CapsuleCollider>();

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiSpawnState());
        stateMachine.RegisterState(new AiWanderState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
        //text.text = stateMachine.currentState.ToString();
        distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
        //animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        if (startTime < 5.0f) 
        { 
            startTime += Time.deltaTime; 
        }
    }

    public void stopEnemy()
    {
        navMeshAgent.isStopped = true;
    }

    public void SpawnParticles()
    {
        spawnParticlesInstance = Instantiate(spawnPartricles, transform.position, Quaternion.identity);
    }
}
