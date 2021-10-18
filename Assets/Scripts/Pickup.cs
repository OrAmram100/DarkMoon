using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    public Text myMessage;
    WoodenChestMotion wcm;
    public GameObject keyBox;
    void Start()
    { 
        wcm = GameObject.FindGameObjectWithTag("woodenChestTag").GetComponent<WoodenChestMotion>();

    
    }
        private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Player")
        {
            keyBox.gameObject.SetActive(false);
            StartCoroutine(PickupText());
            myMessage.text = "You Picked up the key!";
            myMessage.gameObject.SetActive(true);
            StartCoroutine(wcm.Unlock());


        }
    }

    private IEnumerator PickupText()
    {
        myMessage.enabled=true;
        yield return new WaitForSeconds(2f);
        myMessage.enabled = false;



    }
}
