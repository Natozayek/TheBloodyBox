using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    [Header("Health properties")] 
    public int value;
    public int InitialHealth = 100;
    public int MaxHealth;

    [Header("Display properties")] 
    public Slider healthBar;


    void Start()
    {
        MaxHealth = InitialHealth;
        healthBar = GetComponentInChildren<Slider>();
        SetHealth();
    }


    public void SetHealth()
    {
        healthBar.value = InitialHealth;
        value = (int)healthBar.value;
    }

    public void TakeDamage(int damage)
    {
        healthBar.value -= damage;

        if (healthBar.value < 0)
        {
            healthBar.value = 0;
        }

        value = (int)healthBar.value;
    }

    public void HealPlayer(int healingAmount)
    {
        healthBar.value += healingAmount;

        if (healthBar.value > MaxHealth)
        {
            healthBar.value = MaxHealth;
        }

        value = (int)healthBar.value;
    }

    public void IncreasMaxHP()
    {
        
        healthBar.value = MaxHealth * 1.2f;
        value = (int)healthBar.value;
    }
 
}

