using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTileToGoblin : MonoBehaviour
{
    [SerializeField]
    float damage = 10f;

    Rigidbody rb;

    [SerializeField]
    float speed = 500f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Goblin").transform;
        // Vector3 bulletAcuracy = new Vector3(Random.Range(0, 0.1f), Random.Range(0, 0.1f), Random.Range(0, 0.1f));
        Vector3 direction = (target.position - transform.position);
        rb.AddForce(direction * speed * Time.deltaTime);


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Goblin")
        {
            GoblinHealth goblinHealth = collision.transform.GetComponent<GoblinHealth>();
            goblinHealth.DetuctHealth(damage);
            Destroy(gameObject, 3);
        }
        else
        {
            Destroy(gameObject, 3);
        }
    }

}
