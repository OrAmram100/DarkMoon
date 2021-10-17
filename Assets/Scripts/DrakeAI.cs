using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DrakeAI : MonoBehaviour
{
    [SerializeField]
    public GameObject projectTile;
    [SerializeField]
    public Transform shootPoint;
    [SerializeField]
    float turnSpeed = 20;
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



    public Transform target;
    float fireRate = 0.2f;

    AudioSource shootAs;
    private void Awake()
    {
        Transform gunFortake = GameObject.FindGameObjectWithTag("GunToTake").transform;
        gunForTake = gunFortake;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        shootAs = GetComponent<AudioSource>();
        muzzleFlash.Stop();
      //  gunForTake = GameObject.FindGameObjectWithTag("GunToTake").transform;
    }

    void Update()
    {
        if (!GunManager.instance.isGrabbed)
        {
            float distanceToGun = Vector3.Distance(transform.position, gunForTake.transform.position);
            Vector3 directionToGun = gunForTake.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToGun), turnSpeed * Time.deltaTime);
            if (distanceToGun < 800 && !GunManager.instance.isGrabbed)
            {
                agent.isStopped = false;
                agent.updateRotation = true;
                agent.updatePosition = true;
                agent.speed = 40;
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsShooting", false);
                agent.SetDestination(gunForTake.position);
            }
        }
        else if (GunManager.instance.isGrabbed && !isDead && !DrakeHealth.singelton.isEnemyDead)
        {
            fireRate -= Time.deltaTime;
            float distance = Vector3.Distance(transform.position, target.position);
            Vector3 direction = target.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            if (fireRate <= 0 && distance < 300)
            {
                if ((distance >= 40) && (distance <= 100))
                {
                    chasePlayer();
                }
                else if (distance < 40 && canAttack)
                {
                    agent.isStopped = false;
                    agent.updateRotation = false;
                    Vector3 direction2 = target.position - transform.position;
                    direction2.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction2), turnSpeed * Time.deltaTime);
                    agent.updatePosition = false;
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsShooting", false);
                    animator.SetBool("IsAttacking", true);
                    StartCoroutine(AttackTime());

                }
                else if (distance > 100)
                {     //distance between 100-200 stop and shoot
                    agent.isStopped = true;
                    fireRate = 0.5f;
                    shoot();
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
        }
        else
        {
            canAttack = false;
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsAttacking", false);
            gunWhenWalking.gameObject.SetActive(false);
            gunWhenShooting.gameObject.SetActive(false);
            muzzleFlash.Stop();
        }
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
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.singelton.DamagePlayer(damageAmount);
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
        agent.SetDestination(target.position);
    }
    public void enemyDeathAnim()
    {
        isDead = true;
        animator.SetTrigger("IsDead");

    }
}