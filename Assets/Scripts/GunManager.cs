using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;
    public bool isGrabbed = false;
    public GameObject gun;
    BoxCollider trigger;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        trigger = GetComponent<BoxCollider>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Drake")
        {
            Debug.Log("Hi");
            isGrabbed = true;
            gun.SetActive(true);
            trigger.enabled = false;
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
