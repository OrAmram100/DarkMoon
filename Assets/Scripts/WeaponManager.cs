using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;

    // Start is called before the first frame update
    private void Start()
    {
     //   unequickWeapons();
    }
    private void Update()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (Input.GetKeyDown(KeyCode.Alpha1) && PlayerMovement.singelton.isMachineGunGrabbed)
        {
            MachineGun.instance.updateAmmoUI();
            equickPistol();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerMovement.singelton.isGunGrabbed)
        {
            Gun.instance.updateAmmoUI();
            equickMachineGun();
        }

    }
    void unequickWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }
    void equickPistol()
    {
        unequickWeapons();
        weapons[0].SetActive(true);
    }
    void equickMachineGun()
    {
        unequickWeapons();
        weapons[1].SetActive(true);
    }
}
