using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrakesSpawn : MonoBehaviour
{
    public GameObject DrakeToSpawn;
    public Transform[] spawnPoints;
    BoxCollider trigger;

    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpawnEnemies();
            trigger.enabled = false;
        }
    }
    void SpawnEnemies()
    {
        foreach (var sp in spawnPoints)
        {
            Instantiate(DrakeToSpawn, sp.position, sp.rotation);
        }
    }

}
