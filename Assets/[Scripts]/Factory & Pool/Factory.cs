using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factory : MonoBehaviour
{
    // Objects prefabs
    public GameObject bulletPrefab;
    public GameObject EnemyPrefab;


    // Parents
    [SerializeField]private Transform bulletParent;
    [SerializeField]private Transform enemyParent;



    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        enemyParent = GameObject.Find("Enemy").transform;
        bulletParent = GameObject.Find("Bullet").transform;
      
    }

    public GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.SetActive(false);
        return bullet;
    }


    public GameObject CreateEnemy()
    {
        GameObject enemyPrefab = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity, enemyParent);
        enemyPrefab.SetActive(false);
        return enemyPrefab;
    }

}
