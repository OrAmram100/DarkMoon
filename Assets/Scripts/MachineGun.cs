using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public Transform shootPoint;
    public int currentAmmo;
    public float rateOfFire;
    float nextFire = 0;
    public float weaponRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
        {
            shoot();
        }
    }

    void shoot()
    {
        RaycastHit hit;
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateOfFire;
            currentAmmo--;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange))
            {
                if (hit.transform.tag == "Enemy")
                {
                    Debug.Log("Hit enemy");
                }
                else
                {
                    Debug.Log("Hit Something else");

                }
            }
        }
    }



}
