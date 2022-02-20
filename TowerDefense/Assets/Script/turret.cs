using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class turret : MonoBehaviour
{
    [Header("Général")]
    public float range = 15f;
    private Transform target;
    private Enemy targetEnemy;

    [Header("Use Bullets")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    [Header("Use Laser")] 
    public bool useLaser;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpact;
    public Light lightImpact;
    public int damageOverTime = 30;
    public float slowAmout = 0.5f;
    
    [Header("Unity Setup")]
    public String enemyTag = "Enemy";
    public Transform partToRotate;
    private float turnSpeed = 7f;
    public Transform firePoint;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    
    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpact.Stop();
                    lightImpact.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
    
    void UpdateTarget()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in ennemies)
        {
            float distanceToEnnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnnemy < shortestDistance)
            {
                shortestDistance = distanceToEnnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    
    public void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Laser()
    {
        Debug.Log("Entrée Laser");
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmout);
        
        if (lineRenderer.enabled == false)
        {
            Debug.Log("line enabled false");
            lineRenderer.enabled = true;
            laserImpact.Play();
            lightImpact.enabled = true;
        }
        
        Debug.Log("after if");
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        laserImpact.transform.rotation = Quaternion.LookRotation(dir);
        
        laserImpact.transform.position = target.position + dir.normalized * 1.5f;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet bullet = bulletGO.GetComponent<bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
