﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Menu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Laddar upp nästa scen i ordningen när man klickar på play
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
