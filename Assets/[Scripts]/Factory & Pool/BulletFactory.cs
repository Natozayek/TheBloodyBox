using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{
    // Bullet Prefab
    public GameObject bulletPrefab;

    // Sprite Textures
    private Sprite playerBulletSprite;
    private Sprite enemyBulletSprite;

    // Bullet Parent
    private Transform bulletParent;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        bulletParent = GameObject.Find("Bullets").transform;
    }

    public GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.SetActive(false);
        return bullet;
    }

}
