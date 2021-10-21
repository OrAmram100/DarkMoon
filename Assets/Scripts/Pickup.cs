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
            myMessage.gameObject.SetActive(true);
            keyBox.gameObject.SetActive(false);
            myMessage.text = "You Picked up the key!";
            StartCoroutine(PickupText());
            StartCoroutine(wcm.Unlock());

        }
    }

    private IEnumerator PickupText()
    {
        myMessage.enabled = true;
        yield return new WaitForSeconds(5f);
        myMessage.enabled = false;



    }
}
