using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodenChestMotion : MonoBehaviour
{
    private Animator animator;
    public GameObject camera;
    public GameObject seeThroughCrosshair;
    public GameObject touchCrosshair;
    public Text whatToDo;
    public Text findKeyText;
    private bool isFocusOn = false, isLocked = true;
    private AudioSource audioSource;
    public Transform machineGun;
    public Transform machineGun2;
    RaycastHit hit;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetBool("isOpen", false);
        machineGun.GetComponent<BoxCollider>().enabled = false;
        machineGun2.GetComponent<BoxCollider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // check what object is in our focus
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {

            // check that the object in camera focus is THIS
            if (this.transform.gameObject.tag == hit.collider.tag && hit.distance < 25)
            {
                bool isOpen = hit.transform.gameObject.GetComponent<Animator>().GetBool("isOpen");
                if (!isOpen)
                {
                    // change crosshair
                    if (!isFocusOn)
                    {
                        seeThroughCrosshair.SetActive(false);
                        touchCrosshair.SetActive(true);
                        isFocusOn = true;
                    }

                    whatToDo.text = "Press [I] to open the chest";
                    whatToDo.gameObject.SetActive(true);
                    whatToDo.enabled = true;

                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        if (!isLocked)
                            openTheChest();
                        else
                        {
                            whatToDo.enabled = false;
                            findKeyText.text = "You need a key to open it";
                            StartCoroutine(FindKeyText());

                        }

                    }
                }
            }
            else
            {
                isFocusOn = false;
                seeThroughCrosshair.SetActive(true);
                touchCrosshair.SetActive(false);
                whatToDo.gameObject.SetActive(false);
            }
        }





    }

    private IEnumerator FindKeyText()
    {
        findKeyText.gameObject.SetActive(true);
        whatToDo.enabled = false;
        findKeyText.enabled = true;
        yield return new WaitForSeconds(5f);
        findKeyText.enabled = false;
    }
    public IEnumerator Unlock()
    {
        yield return new WaitForSeconds(0.1f);
        isLocked = false;
    }


    private void openTheChest()
    {
        hit.transform.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
        whatToDo.gameObject.SetActive(false);
        audioSource.PlayDelayed(0.5f);
        seeThroughCrosshair.SetActive(true);
        touchCrosshair.SetActive(false);
      //  machineGun.GetComponent<BoxCollider>().enabled = true;
        machineGun2.GetComponent<BoxCollider>().enabled = true;
    }
}


