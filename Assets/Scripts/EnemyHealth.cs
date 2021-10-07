using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;

    public void DetuctHealth(float detuctHealth)
    {
        enemyHealth -= detuctHealth;
        if (enemyHealth <= 0)
        {
            EnemeDead();
        }
    }
    void EnemeDead()
    {
        Destroy(gameObject);
    }

}
