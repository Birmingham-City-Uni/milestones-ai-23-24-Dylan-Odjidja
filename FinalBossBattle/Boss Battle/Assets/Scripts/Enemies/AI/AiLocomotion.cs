using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    public GameObject player;
    public float maxTime = 1.0f;
    public float minDistance = 2.0f;

    NavMeshAgent agent;
    float timer = 0.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (player.transform.position - agent.destination).sqrMagnitude;
            if (sqDistance > minDistance * minDistance)
            {
                agent.destination = player.transform.position;
            }
            timer = maxTime;
        }
    }
}
