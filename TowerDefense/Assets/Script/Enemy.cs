using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health = 100f;
    public int worth = 50;
    public float startSpeed = 15f;
    public GameObject deathEffect;
    
    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float ammout)
    {
        health = health - ammout;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float ammout)
    {
        speed = startSpeed * (1f - ammout);
    }

    private void Die()
    {
        playerStats.money += worth;
        GameObject deathParticle = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathParticle, 2f);
        waveSpawner.enemiesAlive--;
        Destroy(gameObject);
        
    }
}


