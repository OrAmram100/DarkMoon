using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinHealth : MonoBehaviour
{
    public static GoblinHealth singelton;
    public float currentHealth;
    public Slider healthSlider;
    public Text healthCounter;

    public float enemyHealth = 100f;
    GoblinAI goblinAI;
    public bool isEnemyDead;
    public Text isDead;

    private void Awake()
    {
        singelton = this;

    }
    private void Start()
    {
        goblinAI = GetComponent<GoblinAI>();
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
                isDead.gameObject.SetActive(true);
                StartCoroutine(ShowMessage());

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
        isEnemyDead = true;
        currentHealth = 0;
        healthSlider.value = 0;
        UpdateHealthCounter();
        goblinAI.enemyDeathAnim();
        EnemyAI.
        Destroy(gameObject, 2);

    }
    private void UpdateHealthCounter()
    {
        healthCounter.text = currentHealth.ToString();
    }
    IEnumerator ShowMessage()
    {
        isDead.text = "Goblin is Dead protect your life!!";
        isDead.enabled = true;
        yield return new WaitForSeconds(1.5f);
        isDead.enabled = false;
        isDead.gameObject.SetActive(false);
    }
}
