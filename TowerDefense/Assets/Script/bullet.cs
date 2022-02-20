using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    private Transform target;

    public float speed = 70f;

    public GameObject impactEffect;

    public float explosionRadius = 0f;

    public float damage = 50;
    
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFram = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFram)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distanceThisFram, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns =(GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
         Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
         foreach (Collider collider in colliders)
         {
             if (collider.tag == "Enemy")
             {
                 Damage(collider.transform);
             }
         } 
    }
    
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Pas de script enemy");
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
