using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasion : MonoBehaviour
{

    public EnemyBehaviour enemyChild;

    public GameObject child1, child2, child3;
    private void Update()
    {
        transform.position = enemyChild.transform.position;
    }
    private void Start()
    {
        child1.transform.position = transform.position;
        child2.transform.position = transform.position;
        child3.transform.position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if(enemyChild != null && enemyChild.gameObject.GetComponent<EnemyBehaviour>()._EnemyType == EnemyType.BASIC)
            {
                enemyChild.gameObject.GetComponent<EnemyBehaviour>().isChasing = false;
                enemyChild.gameObject.GetComponent<EnemyBehaviour>()._Timer = 0;
                enemyChild.GetComponent<EnemyBehaviour>().Evade(other.transform.position);
            }

        }
    }
}
