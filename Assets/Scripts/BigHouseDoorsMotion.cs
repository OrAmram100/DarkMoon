using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHouseDoorsMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource doorSqueak;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        doorSqueak = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Goblin")
        {
            doorSqueak.PlayDelayed(0.7f);
            animator.SetBool("isOpen", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        doorSqueak.PlayDelayed(1f);
        animator.SetBool("isOpen", false);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
