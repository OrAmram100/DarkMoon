using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DrakeAI : MonoBehaviour
{
    [SerializeField]
    public GameObject projectTile;
    public GameObject projectTileToGoblin;
    [SerializeField]
    public Transform shootPoint;
    [SerializeField]
    float turnSpeed = 100;
    public float damageAmount = 5f;
    float attackTime = 2f;
    [SerializeField]
    ParticleSystem muzzleFlash;
    Animator animator;
    [HideInInspector]
    NavMeshAgent agent;
    bool canAttack = true;
    Transform gunForTake;
    public Transform gunWhenWalking;
    public Transform gunWhenShooting;
    bool isDead = false;
    float distanceFromGoblin;
    bool alreadyAttacked = false;
    public GameObject grenade;
    Text text;
    public bool once = true;

    Transform targetPlayer;
    GameObject targetGoblin;
    float fireRate = 0.2f;

    AudioSource shootAs;
    private void Awake()
    {
        text = GameObject.FindGameObjectWithTag("AlertText").GetComponent<Text>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        targetGoblin = GameObject.FindWithTag("Goblin");
        agent = GetComponent<NavMeshAgent>();
        shootAs = GetComponent<AudioSource>();
        muzzleFlash.Stop();
        gunForTake = GameObject.FindGameObjectWithTag("GunToTake").transform;
        //  gunForTake = GameObject.FindGameObjectWithTag("GunToTake").transform;
    }

    void Update()
    {
        if (!GunManager.instance.isGrabbed)
        {
            Vector3 directionToGun = gunForTake.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToGun), turnSpeed * Time.deltaTime);
            agent.isStopped = false;
            agent.updateRotation = true;
            agent.updatePosition = true;
            agent.speed = 40;
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsShooting", false);
            agent.SetDestination(gunForTake.position);
        }
        else if (GunManager.instance.isGrabbed && !isDead && !DrakeHealth.singelton.isEnemyDead && !PlayerHealth.singelton.isDead)
        {
            if (once)
            {
                StartCoroutine(showText());
            }
            float distanceFromPlayer = Vector3.Distance(transform.position, targetPlayer.position);
            if (targetGoblin != null)
            {
                distanceFromGoblin = Vector3.Distance(transform.position, targetGoblin.transform.position);
            }
            if (distanceFromPlayer < distanceFromGoblin || targetGoblin == null)
            {
                fireRate -= Time.deltaTime;
                float distance = Vector3.Distance(transform.position, targetPlayer.position);
                Vector3 direction = targetPlayer.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
                if (fireRate <= 0 && distance < 300)
                {
                    if ((distance >= 70) && (distance <= 100))
                    {
                        chasePlayer();
                    }
                    else if (distance < 70 && canAttack)
                    {
                        agent.isStopped = false;
                        agent.updateRotation = false;
                        Vector3 direction2 = targetPlayer.position - transform.position;
                        direction2.y = 0;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction2), turnSpeed * Time.deltaTime);
                        agent.updatePosition = false;
                        animator.SetBool("IsWalking", false);
                        animator.SetBool("IsShooting", false);
                        animator.SetBool("IsAttacking", true);
                        StartCoroutine(AttackTime());

                    }
                    else if (distance > 100 && distance < 300)
                    {     //distance between 100-200 stop and shoot
                        agent.isStopped = true;
                        fireRate = 0.5f;
                        shoot();
                        if (!alreadyAttacked)
                        {
                            Rigidbody rb = Instantiate(grenade, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                            rb.AddForce(transform.forward * 200f, ForceMode.Impulse);
                            rb.AddForce(transform.up * 20f, ForceMode.Impulse);
                            Invoke(nameof(resetAttack), 5);
                            alreadyAttacked = true;

                        }
                    }
                }
                else if (distance > 300)
                {
                    agent.isStopped = false;
                    if (distance > 300 && GunManager.instance.isGrabbed)
                    {
                        chasePlayer();
                    }

                }

                //else
                //{
                //    canAttack = false;
                //    animator.SetBool("IsWalking", false);
                //    animator.SetBool("IsAttacking", false);
                //    gunWhenWalking.gameObject.SetActive(false);
                //    gunWhenShooting.gameObject.SetActive(false);
                //    muzzleFlash.Stop();
                //}
            }
            else
            {
                if (targetGoblin != null)
                {
                    fireRate -= Time.deltaTime;
                    float distance = Vector3.Distance(transform.position, targetGoblin.transform.position);
                    Vector3 direction = targetGoblin.transform.position - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
                    if (fireRate <= 0 && distance < 300)
                    {
                        if ((distance >= 70) && (distance <= 100))
                        {
                            chaseGoblin();
                        }
                        else if (distance < 70 && canAttack)
                        {
                            agent.isStopped = false;
                            agent.updateRotation = false;
                            Vector3 direction2 = targetGoblin.transform.position - transform.position;
                            direction2.y = 0;
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction2), turnSpeed * Time.deltaTime);
                            agent.updatePosition = false;
                            animator.SetBool("IsWalking", false);
                            animator.SetBool("IsShooting", false);
                            animator.SetBool("IsAttacking", true);
                            StartCoroutine(AttackGoblinTime());

                        }
                        else if (distance > 100 && distance < 300)
                        {     //distance between 100-200 stop and shoot
                            agent.isStopped = true;
                            fireRate = 0.5f;
                            shootToGoblin();
                            if (!alreadyAttacked)
                            {
                                Rigidbody rb = Instantiate(grenade, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                                rb.AddForce(transform.forward * 200f, ForceMode.Impulse);
                                rb.AddForce(transform.up * 20f, ForceMode.Impulse);
                                Invoke(nameof(resetAttack), 30);
                                alreadyAttacked = true;

                            }
                        }
                    }
                    else if (distance > 300)
                    {
                        agent.isStopped = false;
                        if (distance > 300 && GunManager.instance.isGrabbed)
                        {
                            chaseGoblin();
                        }

                    }

                    //else
                    //{
                    //    canAttack = false;
                    //    animator.SetBool("IsWalking", false);
                    //    animator.SetBool("IsAttacking", false);
                    //    gunWhenWalking.gameObject.SetActive(false);
                    //    gunWhenShooting.gameObject.SetActive(false);
                    //    muzzleFlash.Stop();
                    //}
                }

            }
        }
    }

    IEnumerator showText()
    {
        text.gameObject.SetActive(true);
        text.text = "Be careful! the drake got the gun !";
        text.enabled = true;
        yield return new WaitForSeconds(3f);
        text.enabled = false;
        once = false;
    }

    private void resetAttack()
    {
        alreadyAttacked = false;
    }

    void shoot()
    {
        gunWhenShooting.gameObject.SetActive(true);
        gunWhenWalking.gameObject.SetActive(false);
        animator.SetBool("IsShooting", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        Instantiate(projectTile, shootPoint.position, shootPoint.rotation);
        shootAs.Play();
        muzzleFlash.Play();
    }
    void shootToGoblin()
    {
        gunWhenShooting.gameObject.SetActive(true);
        gunWhenWalking.gameObject.SetActive(false);
        animator.SetBool("IsShooting", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        Instantiate(projectTileToGoblin, shootPoint.position, shootPoint.rotation);
        shootAs.Play();
        muzzleFlash.Play();
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
    void chasePlayer()
    {
        gunWhenWalking.gameObject.SetActive(true);
        gunWhenShooting.gameObject.SetActive(false);
        agent.isStopped = false;
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.speed = 40;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsShooting", false);
        agent.SetDestination(targetPlayer.position);
    }
    void chaseGoblin()
    {
        gunWhenWalking.gameObject.SetActive(true);
        gunWhenShooting.gameObject.SetActive(false);
        agent.isStopped = false;
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.speed = 40;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsShooting", false);
        agent.SetDestination(targetGoblin.transform.position);
    }
    public void enemyDeathAnim()
    {
        isDead = true;
        animator.SetTrigger("IsDead");

    }
}