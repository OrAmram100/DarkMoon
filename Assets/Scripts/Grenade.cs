using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    float countDown;
    public float force = 700f;
    bool hasExploded = false;
    public ParticleSystem explosionEffect;
    public float damage = 40;
    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            StartCoroutine(Explode());
            hasExploded = true;
        }
    }

    IEnumerator Explode()
    {
        explosionEffect.Play();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearByObject in colliders)
        {
            Rigidbody rb = nearByObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearByObject in collidersToMove)
        {
            Rigidbody rb = nearByObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        explosionEffect.Stop();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Enemy")
    //    {
    //        EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();
    //        enemyHealth.DetuctHealth(damage);
    //        Destroy(gameObject, 3);
    //    }
    //    else if (collision.transform.tag == "Enemy")
    //    {
    //        EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();
    //        enemyHealth.DetuctHealth(damage);
    //        Destroy(gameObject, 3);
    //    }
    //}
}
