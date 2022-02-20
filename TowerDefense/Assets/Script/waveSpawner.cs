using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public wave[] wavesArray;

    [SerializeField]
    private float timeBetweenWaves = 4f;

    private float countdown = 5f;

    private int waveNumber = 0;

    [SerializeField]
    private Text waveCountDownTimer;

    [SerializeField]
    private Transform spawnPoint;
    
    // Update is called once per frame
    void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountDownTimer.text = string.Format("{0:0.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        playerStats.waves++;

        wave wave = wavesArray[waveNumber];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }
        
        waveNumber++;

        if (waveNumber == wavesArray.Length)
        {
            Debug.Log("Niveau Terminé");
            this.enabled = false;
        }
    }

    void SpawnEnnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
    }
}
