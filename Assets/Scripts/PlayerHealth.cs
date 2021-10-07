using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singelton;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;

    private void Awake()
    {
        singelton = this;

    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
    public void DamagePlayer(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            Dead();
        }
    }
    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("Player is dead");

    }
}
