using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public static int money;
    public int startMoney = 400;
    public static int lives;
    public int startLives = 20;

    public static int waves;
    
    
    // Start is called before the first frame update
    void Start()
    {
        waves = 0;
        money = startMoney;
        lives = startLives;
    }
    
}
