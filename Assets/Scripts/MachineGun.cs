using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : MonoBehaviour
{
    //used to damage enemy
    public float damageEnemy = 20f;

    //weapon effect
    public ParticleSystem muzzleFlash;
    //sound
    private AudioSource gunAs;
    public AudioClip shootAC;
    public AudioClip dryFireAC;
    //eject bullet casing
    public ParticleSystem bulletCasing;
    //blood effect
    public GameObject bloodEffect;
    RaycastHit hit;

    public Transform shootPoint;

    public Text currentAmmoText;
    public Text carriedAmmoText;

    
    public int currentAmmo = 12;
    public int maxAmmo = 12;
    public int carriedAmmo = 60;
    bool isReloading;
    public float rateOfFire;
    float nextFire = 0;
    public float weaponRange;

    Animator anim;

    private void Start()
    {
        muzzleFlash.Stop();
        gunAs = GetComponent<AudioSource>();
        bulletCasing.Stop();
        anim = GetComponent<Animator>();
        updateAmmoUI();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
        {
            shoot();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo <= 0 && !isReloading)
        {
            DryFire();
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo <= maxAmmo && !isReloading)
        {
            isReloading = true;
            Reload();
        }

    }

    void shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = 0f;
            nextFire = Time.time + rateOfFire;
            anim.SetTrigger("Shoot");
            currentAmmo--;
            gunAs.volume = 0.2f;
            gunAs.PlayOneShot(shootAC);
            StartCoroutine(WeaponEffect());
            ShootRay();
            updateAmmoUI();

        }
    }
    void updateAmmoUI()
    {
        currentAmmoText.text = currentAmmo.ToString();
        carriedAmmoText.text = carriedAmmo.ToString();
    }
    void ShootRay()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange))
        {
            if (hit.transform.tag == "Enemy")
            {
                //Debug.Log("Hit enemy");
                EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                enemyHealthScript.DetuctHealth(damageEnemy);
                Instantiate(bloodEffect, hit.point, transform.rotation);
            }
            else
            {
                Debug.Log(hit.transform.name);

            }
        }
    }
    void DryFire()
    {

        if (Time.time > nextFire)
        {
            nextFire = 0f;
            nextFire = Time.time + rateOfFire;
            gunAs.PlayOneShot(dryFireAC);

        }

    }
    void Reload()
    {

        if (carriedAmmo <= 0)
        {
            return;
        }
        anim.SetTrigger("Reload");
        StartCoroutine(ReloadCountDown(2f));

    }
    IEnumerator ReloadCountDown(float timer)
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;

        }
        if (timer <= 0f)
        {
            int bulletsNeededToFillmag = maxAmmo - currentAmmo;
            int bulletsToDeduct = (carriedAmmo >= bulletsNeededToFillmag) ? bulletsNeededToFillmag : carriedAmmo;

            carriedAmmo -= bulletsToDeduct;
            currentAmmo += bulletsToDeduct;
            isReloading = false;
            updateAmmoUI();
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




