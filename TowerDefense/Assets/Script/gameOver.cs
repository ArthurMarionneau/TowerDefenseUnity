using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    public Text wavesText;

    private void OnEnable()
    {
        wavesText.text = playerStats.waves.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        
    }
}
