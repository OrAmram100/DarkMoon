using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DrakeAI : MonoBehaviour
{
    [SerializeField]
    public GameObject projectTile;
    public GameObject projectTileToGoblin;
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
    float distanceFromGoblin;


    Transform targetPlayer;
    Transform targetGoblin;
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
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        targetGoblin = GameObject.FindGameObjectWithTag("Goblin").transform;
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
            float distanceFromPlayer = Vector3.Distance(transform.position, targetPlayer.position);
            if (targetGoblin != null)
            {
                distanceFromGoblin = Vector3.Distance(transform.position, targetGoblin.position);
            }
            if (distanceFromPlayer < distanceFromGoblin)
            {
                fireRate -= Time.deltaTime;
                float distance = Vector3.Distance(transform.position, targetPlayer.position);
                Vector3 direction = targetPlayer.position - transform.position;
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
                        Vector3 direction2 = targetPlayer.position - transform.position;
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
            else
            {
                if (targetGoblin != null)
                {
                    fireRate -= Time.deltaTime;
                    float distance = Vector3.Distance(transform.position, targetGoblin.position);
                    Vector3 direction = targetGoblin.position - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
                    if (fireRate <= 0 && distance < 300)
                    {
                        if ((distance >= 40) && (distance <= 100))
                        {
                            Debug.Log("chase after roni");
                            chaseGoblin();
                        }
                        else if (distance < 40 && canAttack)
                        {
                            agent.isStopped = false;
                            agent.updateRotation = false;
                            Vector3 direction2 = targetGoblin.position - transform.position;
                            direction2.y = 0;
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction2), turnSpeed * Time.deltaTime);
                            agent.updatePosition = false;
                            animator.SetBool("IsWalking", false);
                            animator.SetBool("IsShooting", false);
                            animator.SetBool("IsAttacking", true);
                            StartCoroutine(AttackGoblinTime());

                        }
                        else if (distance > 100)
                        {     //distance between 100-200 stop and shoot
                            Debug.Log("shoot after roni");
                            agent.isStopped = true;
                            fireRate = 0.5f;
                            shootToGoblin();
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

            }
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
        agent.SetDestination(targetGoblin.position);
    }
    public void enemyDeathAnim()
    {
        isDead = true;
        animator.SetTrigger("IsDead");

    }
}