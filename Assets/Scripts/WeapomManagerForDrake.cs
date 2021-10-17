using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapomManagerForDrake : MonoBehaviour
{
    public GameObject[] weapons;

    // Start is called before the first frame update
    private void Start()
    {
        unequickWeapons();
    }
    void unequickWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }
}
