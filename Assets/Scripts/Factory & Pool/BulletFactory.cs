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

    public GameObject CreateBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletBehaviour>().bulletType = type;

        switch (type)
        {
            case BulletType.SPIRAL:

                bullet.name = "SPIRAL";
                Debug.Log("CreatedBullet(SPIRAL)");
                break;
            case BulletType.SINGLE:

                bullet.name = "SINGLE";
                Debug.Log("CreatedBullet(SPIRAL)");
                break;
          
            case BulletType.SHOTGUN:

                bullet.name = "SHOTGUN";
                Debug.Log("CreatedBullet(SPIRAL)");
                break;
                #region
                //case BulletType.ENEMY:
                //    bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                //    bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.LEFT);
                //    bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                //    bullet.name = "EnemyBullet";
                //    break;
                #endregion

        }

        bullet.SetActive(false);
        return bullet;
    }

}
