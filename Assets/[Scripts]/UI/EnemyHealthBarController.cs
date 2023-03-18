using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour
{
    [Header("Health properties")]
    public int value;

    public EnemyBehaviour enemyPrefab;

    [Header("Display properties")]
    public Slider healthBar;

    void Start()
    {
       // enemyPrefab = GetComponentInParent<EnemyBehaviour>();
        healthBar = GetComponentInChildren<Slider>();
        resetHeath();
    }


    public void resetHeath()
    {
        healthBar.maxValue = enemyPrefab._MaxtHealth;
        healthBar.value = enemyPrefab._MaxtHealth;
        value = (int)healthBar.value;
    }

    public void TakeDamage(float damage)
    {
        healthBar.value -= damage;

        if (healthBar.value < 0)
        {
            healthBar.value = 0;
        }

        value = (int)healthBar.value;
    }

    public void HealPlayer(float healingAmount)
    {
        healthBar.value += healingAmount;

        if (healthBar.value > 100)
        {
            healthBar.value = 100;
        }

        value = (int)healthBar.value;
    }


}
