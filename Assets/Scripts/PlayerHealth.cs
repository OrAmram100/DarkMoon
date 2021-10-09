using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singelton;
    public float currentHealth;
    public float maxHealth = 100f;
    public Slider healthSlider;
    public Text healthCounter;
    public bool isDead = false;

    private void Awake()
    {
        singelton = this;

    }
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthCounter();

    }




    public void DamagePlayer(float damage)
    {
        if (currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                Dead();
            }
            else
            {
                currentHealth -= damage;
                healthSlider.value -= damage;
            }
            UpdateHealthCounter();
        }
    }
    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        healthSlider.value = 0;
        UpdateHealthCounter();
        Debug.Log("Player is dead");

    }
    private void UpdateHealthCounter()
    {
        healthCounter.text = currentHealth.ToString();
    }
}