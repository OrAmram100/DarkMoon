using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;
    public bool isGrabbed = false;
    BoxCollider trigger;
    public bool isPlayerGrabbed = false;


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
            isGrabbed = true;
            trigger.enabled = false;
            gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
