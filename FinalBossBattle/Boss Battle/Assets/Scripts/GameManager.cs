using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Health health;
    public GameObject boss;
    private Health bossHealth;
    private BehaviourTreeRunner BTRunner;
    public int winIndex;
    public int loseIndex;
    public int minions;
    public bool paused = false;
    public GameObject pauseMenu;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<Health>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossHealth = boss.GetComponent<Health>();
        BTRunner = boss.GetComponent<BehaviourTreeRunner>();
        minions = 7;
    }

    void Update()
    {
        if (!player)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(loseIndex);
        }

        if (minions <= 0) 
        { 
            BTRunner.enabled = true;
        }

        if (bossHealth.currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(winIndex);
        }

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            health.currentHealth -= 50;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            paused = false;
        }
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(2);
    }
}
