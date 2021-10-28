using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinProjectTile : MonoBehaviour
{
    public static GoblinProjectTile singelton;
    GameObject goblin;
    [SerializeField]
    float damage = 3f;

    Rigidbody rb;

    [SerializeField]
    float speed = 500f;
    GameObject[] zombies;
    GameObject closetEnemy = null;

    // public Transform drake;
    private void Awake()
    {
        singelton = this;
        rb = GetComponent<Rigidbody>();
        goblin = GameObject.FindGameObjectWithTag("Goblin");
    }
    private void Update()
    {
        findClosetEnemy();

    }

    private void findClosetEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject currentEnemy in zombies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closetEnemy = currentEnemy;
                Vector3 direction = (closetEnemy.transform.position - transform.position);
                //goblin.transform.rotation = Quaternion.Slerp(goblin.transform.rotation, Quaternion.LookRotation(direction), 1 * Time.deltaTime);
                rb.AddForce(direction * speed * Time.deltaTime);
            }
        }
      //  Debug.DrawLine(this.transform.position, closetEnemy.transform.position);
    }

    //public void findGameObject(GameObject gameObject)
    //{
    //    if (gameObject.tag == "Enemy")
    //    {
    //        Debug.Log(gameObject.transform.position);
    //    }
    //    else if (gameObject.tag == "Drake")
    //    {
    //      //  drake = gameObject.transform;
    //    }
    //}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Drake")
        {
            Debug.Log("ball");
            DrakeHealth drakeHealth = collision.transform.GetComponent<DrakeHealth>();
            DrakeHealth.singelton.DetuctHealth(damage);
            Destroy(gameObject, 3);
        }
        else if (collision.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();
            enemyHealth.DetuctHealth(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 2);
        }
    }

}
