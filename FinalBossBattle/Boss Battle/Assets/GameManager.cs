using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    void Update()
    {
        if (!player)
        {
            Time.timeScale = 0;
            Debug.Log("You Lose");
        }
        if (!boss)
        {
            Time.timeScale = 0;
            Debug.Log("You Win");
        }
    }
}
