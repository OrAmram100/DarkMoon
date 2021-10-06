using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            float x, y, z;
            x = transform.forward.x * 10;
            y = transform.forward.y;
            z = transform.forward.z * 10;


            rb.AddForce(x, y, z, ForceMode.Impulse);
        }
    }
}
