using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasion : MonoBehaviour
{
    public EnemyBehaviour enemyChild;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))

        {
            Debug.Log("Start evade;");
            
            enemyChild.GetComponent<EnemyBehaviour>().Evade(other.transform.position);
        }
    }
}
