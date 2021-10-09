using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;
    EnemyAI enemyAI;
    public bool isEnemyDead;
    public Collider[] enemyCol; 

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
    void EnemeDead()
    {
        isEnemyDead = true;
        enemyAI.enemyDeathAnim();
        enemyAI.agent.speed = 0f;
        foreach (var col in enemyCol)
        {
            col.enabled = false;
        }
        Destroy(gameObject, 10);
    }

}
