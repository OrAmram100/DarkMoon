using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    //used to damage enemy
    public float damageEnemy = 20f;

    //weapon effect
    public ParticleSystem muzzleFlash;
    //sound
    private AudioSource gunAs;
    public AudioClip shootAC;
    //eject bullet casing
    public ParticleSystem bulletCasing;

    public Transform shootPoint;
    public int currentAmmo;
    public float rateOfFire;
    float nextFire = 0;
    public float weaponRange;

    private void Start()
    {
        muzzleFlash.Stop();
        gunAs = GetComponent<AudioSource>();
        bulletCasing.Stop();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
        {
            shoot();
        }
    }

    void shoot()
    {
        RaycastHit hit;
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateOfFire;
            currentAmmo--;
            gunAs.volume = 0.2f;
            gunAs.PlayOneShot(shootAC);
            StartCoroutine(WeaponEffect());
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange))
            {
                if (hit.transform.tag == "Enemy")
                {
                    //Debug.Log("Hit enemy");
                    EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                    enemyHealthScript.DetuctHealth(damageEnemy);
                }
                else
                {
                    Debug.Log("Hit Something else");

                }
            }
        }
        IEnumerator WeaponEffect()
        {
            bulletCasing.Play();
            muzzleFlash.Play();
            yield return new WaitForEndOfFrame();
            muzzleFlash.Stop();
            bulletCasing.Stop();
        }
    }



}
