using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasion : MonoBehaviour
{

    public EnemyBehaviour enemyChild;

    private void Update()
    {
        transform.position = enemyChild.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))

        {
            enemyChild.GetComponent<EnemyBehaviour>().Evade(other.transform.position);
        }
    }
}
