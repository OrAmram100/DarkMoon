using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpen", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen", true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

