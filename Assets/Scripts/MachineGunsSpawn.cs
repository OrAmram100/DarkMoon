using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunsSpawn : MonoBehaviour
{
    public GameObject weaponToSpawn;
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
            SpawnWeapons();
            trigger.enabled = false;
        }
    }
    void SpawnWeapons()
    {
        foreach (var sp in spawnPoints)
        {
            Instantiate(weaponToSpawn, sp.position, sp.rotation);
        }
    }

}