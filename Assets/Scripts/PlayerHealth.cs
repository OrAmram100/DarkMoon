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

    public Color damageColor;
    public Image damageImage;
    float colorSmopthing = 6f;
    bool isTakingDamage = false;

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

    private void Update()
    {
        if (isTakingDamage)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmopthing * Time.deltaTime);
        }
        isTakingDamage = false;
    }


    public void DamagePlayer(float damage)
    {
        if (currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                isTakingDamage = true;
                Dead();
            }
            else
            {
                isTakingDamage = true;
                currentHealth -= damage;
            }
            UpdateHealthCounter();
        }
    }
    public void addHealth(float healthAmmount)
    {
        currentHealth += healthAmmount;
        UpdateHealthCounter();

    }
    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        healthSlider.value = 0;
        UpdateHealthCounter();
        Debug.Log("Player is dead");

    }
    public void UpdateHealthCounter()
    {
        healthCounter.text = currentHealth.ToString();
        healthSlider.value = currentHealth;

    }
}