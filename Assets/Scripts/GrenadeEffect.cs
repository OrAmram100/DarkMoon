using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEffect : MonoBehaviour
{
    public float damage = 40;
    Collider enemyCol;

    void Start()
    {
        enemyCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();
            enemyHealth.DetuctHealth(damage);
            enemyCol.enabled = false;
        }
        else if (collision.transform.tag == "Drake")
        {
            DrakeHealth drakeHealth = collision.transform.GetComponent<DrakeHealth>();
            drakeHealth.DetuctHealth(damage);
            enemyCol.enabled = false;
        }
        else if (collision.transform.tag == "Player")
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.DamagePlayer(40);
            enemyCol.enabled = false;
        }
        else if (collision.transform.tag == "Goblin")
        {
            GoblinHealth goblinHealth = collision.transform.GetComponent<GoblinHealth>();
            goblinHealth.DetuctHealth(40);
            enemyCol.enabled = false;
        }
        enemyCol.enabled = false;
    }
}
