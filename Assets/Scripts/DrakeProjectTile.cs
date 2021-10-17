using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrakeProjectTile : MonoBehaviour
{
    public static DrakeProjectTile singelton;
    GameObject goblin;
    [SerializeField]
    float damage = 3f;

    Rigidbody rb;

    [SerializeField]
    float speed = 500f;
    GameObject[] drakes;
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
        Debug.Log("Hi");
        findClosetEnemy();

    }

    private void findClosetEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        drakes = GameObject.FindGameObjectsWithTag("Drake");
        Debug.Log(drakes.Length);
        foreach (GameObject currentEnemy in drakes)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closetEnemy = currentEnemy;
                Vector3 direction = (closetEnemy.transform.position - transform.position);
               // goblin.transform.rotation = Quaternion.Slerp(goblin.transform.rotation, Quaternion.LookRotation(direction), 20 * Time.deltaTime);
                rb.AddForce(direction * speed * Time.deltaTime);
                Debug.Log(closetEnemy.transform.name);
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
            Debug.Log("hit");
            DrakeHealth drakeHealth = collision.transform.GetComponent<DrakeHealth>();
            drakeHealth.DetuctHealth(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3);
        }
    }

}
