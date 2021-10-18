using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 1f;
    public GameObject grenadePrefab;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && PlayerMovement.singelton.numOfCurrentGrenades > 0)
        {
            ThrowGrenade();
            PlayerMovement.singelton.numOfCurrentGrenades = PlayerMovement.singelton.numOfCurrentGrenades - 1;
            PlayerMovement.singelton.numOfGrenadesText.text = "Grenades: " + PlayerMovement.singelton.numOfCurrentGrenades.ToString();
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
