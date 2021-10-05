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
    private bool  isFocusOn = false,isLocked=true;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetBool("isOpen", false);

    }

    // Update is called once per frame
    void Update()
    {
        bool isOpen = animator.GetBool("isOpen");
        RaycastHit hit;
        // check what object is in our focus
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) && !isOpen)
        {

            // check that the object in camera focus is THIS
            if (this.transform.gameObject.name == hit.collider.name && hit.distance < 25)
            {
                // change crosshair
                if (!isFocusOn)
                {
                    seeThroughCrosshair.SetActive(false);
                    touchCrosshair.SetActive(true);
                    isFocusOn = true;
                }

                whatToDo.text = "Press [E] to open the chest";
                whatToDo.gameObject.SetActive(true);
                whatToDo.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
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
       yield return  new WaitForSeconds(0.1f);
        isLocked = false;
    }
   

    private void openTheChest()
    {
        whatToDo.gameObject.SetActive(false);
        audioSource.PlayDelayed(0.5f);
        animator.SetBool("isOpen", true);
        seeThroughCrosshair.SetActive(true);
        touchCrosshair.SetActive(false);
    }
}


