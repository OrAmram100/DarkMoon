using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrakeHealth : MonoBehaviour
{
    public static DrakeHealth singelton;
    public float currentHealth;
    public Slider healthSlider;
    public Text healthCounter;

    public float enemyHealth = 100f;
    DrakeAI drakeAI;
    public bool isEnemyDead;

    private void Awake()
    {
        singelton = this;

    }
    private void Start()
    {
        drakeAI = GetComponent<DrakeAI>();
        currentHealth = enemyHealth;
        healthSlider.value = enemyHealth;
        UpdateHealthCounter();
    }
    public void DetuctHealth(float damage)
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
    public void Dead()
    {
        GoblinAI.singelton.drakeEnter = false;
        isEnemyDead = true;
        currentHealth = 0;
        healthSlider.value = 0;
        UpdateHealthCounter();
        drakeAI.enemyDeathAnim();
        PlayerMovement.killCounter++;
        Destroy(gameObject, 2);

    }
    private void UpdateHealthCounter()
    {
        healthCounter.text = currentHealth.ToString();
    }
}
