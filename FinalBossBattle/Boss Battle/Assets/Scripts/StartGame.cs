using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void Restart(int index)
    {
        SceneManager.LoadScene(index);
    }
}
