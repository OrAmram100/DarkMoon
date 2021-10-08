using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public GameObject target;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            animator.SetInteger("state", 1);
            agent.SetDestination(target.transform.position);

        }
    }
}