using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public GameObject camera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);
        //  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}