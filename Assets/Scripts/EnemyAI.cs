using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    Transform targetPlayer;
    GameObject targetGoblin;
    [HideInInspector]
    public NavMeshAgent agent;
    Animator animator;
    bool isDead = false;
    public bool canAttack = true;
    float turnSpeed = 10f;
    public float damageAmount = 30f;
    float attackTime = 2f;
    float distanceFromGoblin;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        targetGoblin = GameObject.FindWithTag("Goblin");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        if (targetGoblin != null)
        {
            distanceFromGoblin = Vector3.Distance(transform.position, targetGoblin.transform.position);
        }
        if (distanceFromGoblin > distance || targetGoblin == null)
        {
            if (!isDead && !PlayerHealth.singelton.isDead)
            {
                if (distance < 50 && canAttack)
                {
                    AttackPlayer();
                }

                else if (distance > 50)
                {
                    ChasePlayer();
                }
            }
            else
            {
                DisableEnemy();
            }
        }
        else
        {
            if (targetGoblin != null)
            {
                if (!isDead)
                {
                    if (distance < 70 && canAttack)
                    {
                        AttackGoblin();
                    }

                    else if (distance > 70)
                    {
                        ChaseGoblin();
                    }
                }
                else
                {
                    DisableEnemy();
                }
            }
        }
    }
    void ChaseGoblin()
    {
        Vector3 direction = targetGoblin.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.transform.position = Vector3.MoveTowards(transform.position, targetGoblin.transform.position, 3f);
        agent.updateRotation = true;
        agent.updatePosition = true;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
    }
    void AttackGoblin()
    {
        agent.updateRotation = false;
        Vector3 direction = targetGoblin.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updatePosition = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", true);
        StartCoroutine(AttackGoblinTime());
    }
    public void enemyDeathAnim()
    {
        isDead = true;
        animator.SetTrigger("IsDead");
    }
    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true;
        Vector3 direction = targetPlayer.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, 3f);
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
    }
    void AttackPlayer()
    {
        agent.updateRotation = false;
        Vector3 direction = targetPlayer.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updatePosition = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", true);
        StartCoroutine(AttackTime());
    }
    void DisableEnemy()
    {
        canAttack = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", false);
    }
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.singelton.DamagePlayer(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;

    }
    IEnumerator AttackGoblinTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        GoblinHealth.singelton.DetuctHealth(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
}
