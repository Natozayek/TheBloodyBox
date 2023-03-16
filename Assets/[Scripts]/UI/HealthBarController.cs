using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    [Header("Health properties")] 
    public float value;
    public float InitialHealth = 100;
    public float MaxHealth;

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

        if (healthBar.value > MaxHealth)
        {
            healthBar.value = MaxHealth;
        }

        value = (int)healthBar.value;
    }

    public void IncreasMaxHP()
    {
        healthBar.maxValue = MaxHealth * 1.2f;
        MaxHealth = MaxHealth * 1.2f;
        InitialHealth = MaxHealth;
        healthBar.value = MaxHealth * 1.2f;

        value = (int)healthBar.value;
    }
 
}

