using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerForGoblin : MonoBehaviour
{
    public GameObject[] weapons;

    // Start is called before the first frame update
    private void Start()
    {
        unequickWeapons();
    }
    private void Update()
    {
        

    }
    void unequickWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }
    
}
