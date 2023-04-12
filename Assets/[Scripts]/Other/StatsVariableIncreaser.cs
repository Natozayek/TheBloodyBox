using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsVariableIncreaser : MonoBehaviour
{
    [Header("Basic Enemy Variables")]
    public float Basic_MaxHealth = 100;
    public float Basic_Speed = 2;

    [Header("Tank Enemy Variables")]
    public float Tank_MaxHealth = 200;
    public float Tank_Speed = 1;

    [Header("Explosive Enemy Variables")]
    public float Explosive_MaxHealth = 50;
    public float Explosive_Speed = 3;

    [Header("Enemy Multiplier Variables")]
    public float _EnemySpeedMultiplier = 1;
    public float _EnemyHealthMultiplier = 1;

    [Header("Bullet Variables")]
    public float _BulletSpeed = 5;
    public float _Bullet_Regular_Damage = 55;
    [Header("Bullet Multiplier Variables")]
    
    public float _BulletSpeed_Multiplier = 1;
    public float _Bullet_Damage_Multiplier = 1;

    public float WaveRound = 1;


}
