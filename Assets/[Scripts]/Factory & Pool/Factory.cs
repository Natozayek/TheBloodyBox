using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factory : MonoBehaviour
{
    // Bullet Prefab
    public GameObject bulletPrefab;
    public GameObject EnemyPrefab;

    // Sprite Textures
    private Sprite playerBulletSprite;

    // Bullet Parent
    private Transform bulletParent;
    private Transform enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        bulletParent = GameObject.Find("Bullets").transform;
        enemyParent = GameObject.Find("Enemy").transform;
    }

    public GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.SetActive(false);
        return bullet;
    }


    public GameObject CreateEnemy()
    {
        GameObject enemyPrefab = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        enemyPrefab.SetActive(false);
        return enemyPrefab;
    }

}
