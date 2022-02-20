using System;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static bool gameIsOver;

    public GameObject gameOverUi;

    private void Start()
    {
        gameOverUi.SetActive(false);
        gameIsOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            EndGame();
        }
        
        if (gameIsOver)
        {
            return;
        }
        if (playerStats.lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOverUi.SetActive(true);
    }
}
