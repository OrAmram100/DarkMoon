using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAI : MonoBehaviour
{
    [SerializeField]
    public GameObject projectTileForZombies;
    [SerializeField]
    public GameObject projectTileForDrakes;
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
    Transform targetToPlayer;
    Transform zombie;
    Transform drake;
    float fireRate = 0.2f;
    AudioSource shootAs;
    public Transform player;
    Rigidbody rb;
    float speed = 500f;
    private bool zombieEnter = false;
    private bool drakeEnter = false;
    public Transform grenade;
    bool alreadyAttacked = false;


    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        shootAs = GetComponent<AudioSource>();
        muzzleFlash.Stop();
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            zombie = GameObject.FindGameObjectWithTag("Enemy").transform;
        }
        if (GameObject.FindGameObjectWithTag("Drake") != null)
        {
            drake = GameObject.FindGameObjectWithTag("Drake").transform;
        }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (!PlayerMovement.singelton.isPlayerGrabbed)
        {
            if (distanceFromPlayer < 100)
            {
                npcStand();
            }
            else
            {
                followPlayer();
            }
        }
        else if (PlayerMovement.singelton.isPlayerGrabbed && !isDead)
        {//&& !DrakeHealth.singelton.isEnemyDead)
            fireRate -= Time.deltaTime;
            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceFromPlayer < 150 && !zombieEnter && !drakeEnter)
            {
                npcStandWithWeapon();
            }
            if (distanceFromPlayer > 150 && distanceFromPlayer <= 200)
            {
                followPlayerWithWeapons();
            }
            else if (distanceFromPlayer > 200)
            {
                runAfterPlayer();
            }
            if (zombie != null)
            {
                float distanceFromZombie = Vector3.Distance(transform.position, zombie.transform.position);
            }
            if (zombie == null)
            {
                zombieEnter = false;
            }
            if (drake != null)
            {
                float distanceFromDrake = Vector3.Distance(transform.position, drake.transform.position);
            }
            if (drake == null)
            {
                drakeEnter = false;
            }
            if (fireRate <= 0 && drakeEnter && drake != null)
            {
                Vector3 direction = drake.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
                //if (distanceFromDrake < 50 && canAttack)
                //{
                //    Debug.Log("attack drake");
                //    goblinAttackDrake();
                //}
                fireRate = 0.5f;
                shootToDrake();
                if (!alreadyAttacked && PlayerMovement.singelton.numOfCurrentGrenades > 0)
                {
                    Vector3 direction2 = drake.position - transform.position;
                    direction2.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction2), turnSpeed * Time.deltaTime);
                    transform.LookAt(drake);
                    Rigidbody rb = Instantiate(grenade, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 20f, ForceMode.Impulse);
                    rb.MovePosition(drake.position);
                    Invoke(nameof(resetAttack), 30);
                    alreadyAttacked = true;

                }
            }
            else if (fireRate <= 0 && zombieEnter && zombie != null)
            {
                Vector3 direction = zombie.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
                //if (distanceFromZombie < 50 && canAttack)
                //{
                //    Debug.Log("attack zombie");
                //    goblinAttackZombie();
                //}

                fireRate = 0.5f;
                shootToZombie();
                if (!alreadyAttacked && PlayerMovement.singelton.numOfCurrentGrenades > 0)
                {
                    Vector3 direction2 = zombie.position - transform.position;
                    direction2.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction2), turnSpeed * Time.deltaTime);
                    transform.LookAt(zombie);
                    Rigidbody rb = Instantiate(grenade, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 20f, ForceMode.Impulse);
                    rb.MovePosition(zombie.position);
                    Invoke(nameof(resetAttack), 5);
                    alreadyAttacked = true;

                }
            }

        }

    }
    private void resetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            zombieEnter = true;
            zombie = other.transform;
        }
        else if (other.transform.tag == "Drake")
        {
            drakeEnter = true;
            drake = other.transform;
        }
    }
    void npcStandWithWeapon()
    {
        gunWhenWalking.gameObject.SetActive(true);
        gunWhenShooting.gameObject.SetActive(false);
        agent.isStopped = true;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsAttacking", false);
    }
    void npcStand()
    {
        agent.isStopped = true;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsAttacking", false);
    }
    void shootToDrake()
    {
        gunWhenShooting.gameObject.SetActive(true);
        gunWhenWalking.gameObject.SetActive(false);
        animator.SetBool("IsShooting", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        Instantiate(projectTileForDrakes, shootPoint.position, shootPoint.rotation);
        shootAs.Play();
        muzzleFlash.Play();
    }
    void shootToZombie()
    {
        gunWhenShooting.gameObject.SetActive(true);
        gunWhenWalking.gameObject.SetActive(false);
        animator.SetBool("IsShooting", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        Instantiate(projectTileForZombies, shootPoint.position, shootPoint.rotation);
        shootAs.Play();
        muzzleFlash.Play();
    }

    void followPlayerWithWeapons()
    {
        agent.isStopped = false;
        gunWhenWalking.gameObject.SetActive(true);
        gunWhenShooting.gameObject.SetActive(false);
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.speed = 50;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsShooting", false);
        agent.SetDestination(player.position);
    }
    void followPlayer()
    {
        agent.isStopped = false;
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.speed = 50;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsShooting", false);
        agent.SetDestination(player.position);
    }

    void runAfterPlayer()
    {
        agent.isStopped = false;
        gunWhenWalking.gameObject.SetActive(true);
        gunWhenShooting.gameObject.SetActive(false);
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.speed = 100;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsShooting", false);
        agent.SetDestination(player.position);
    }
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        DrakeHealth.singelton.DetuctHealth(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;

    }
    IEnumerator zombieAttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        EnemyHealth.singelton.DetuctHealth(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;

    }
    void goblinAttackZombie()
    {
        agent.isStopped = false;
        agent.updateRotation = false;
        Vector3 direction2 = zombie.position - transform.position;
        direction2.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction2), turnSpeed * Time.deltaTime);
        agent.updatePosition = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsAttacking", true);
        StartCoroutine(zombieAttackTime());
    }
    void goblinAttackDrake()
    {
        agent.isStopped = false;
        agent.updateRotation = false;
        Vector3 direction = drake.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updatePosition = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsAttacking", true);
        StartCoroutine(AttackTime());
    }
    public void enemyDeathAnim()
    {
        isDead = true;
        animator.SetTrigger("IsDead");

    }

}