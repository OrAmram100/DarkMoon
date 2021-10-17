using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth singelton;
    public float enemyHealth = 100f;
    EnemyAI enemyAI;
    public bool isEnemyDead = false;
    public Collider[] enemyCol;

    private void Awake()
    {
        singelton = this;
    }
    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    public void DetuctHealth(float detuctHealth)
    {
        if (!isEnemyDead)
        {
            enemyHealth -= detuctHealth;
            if (enemyHealth <= 0)
            {
                EnemeDead();
            }
        }
    }
    public void EnemeDead()
    {
        isEnemyDead = true;
        enemyAI.enemyDeathAnim();
        enemyAI.agent.speed = 0f;
        foreach (var col in enemyCol)
        {
            col.enabled = false;
        }
        enemyHealth = 0f;
        Destroy(gameObject, 2);
    }

}
