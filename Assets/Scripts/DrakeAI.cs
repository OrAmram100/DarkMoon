using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DrakeAI : MonoBehaviour
{
    [SerializeField]
    public GameObject projectTile;
    [SerializeField]
    public Transform shootPoint;
    [SerializeField]
    float turnSpeed = 20;
    [SerializeField]
    ParticleSystem muzzleFlash;


    public Transform target;
    float fireRate = 0.2f;

    AudioSource shootAs;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shootAs = GetComponent<AudioSource>();
        muzzleFlash.Stop();
    }

    void Update()
    {
        fireRate -= Time.deltaTime;

        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

        if (fireRate <= 0)
        {
            fireRate = 0.5f;
            shoot();
        }
    }
    void shoot()
    {
        Instantiate(projectTile, shootPoint.position, shootPoint.rotation);
        shootAs.Play();
        muzzleFlash.Play();
    }
}