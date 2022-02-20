using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class enemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    private Enemy enemy;
    
    private void Start()
    {
        target = waypoint.points[0];
        enemy = GetComponent<Enemy>();
    }
    
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= waypoint.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = waypoint.points[waypointIndex];
    }

    private void EndPath()
    {
        playerStats.lives--;
        waveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
