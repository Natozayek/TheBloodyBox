using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    [Header("Bullet Properties")]
    [Range(10, 1000)]
    public int playerBulletNumber = 100;
    public int playerBulletCount = 0;
    public int activePlayerBullets = 0;

    private BulletFactory factory;
    private Queue<GameObject> playerBulletPool;
    private Queue<GameObject> ShotgunBulletPool;
    public float angle = 0f;

    [Header("Projectile Settings")]
    public float projectileSpeed;               // Speed of the projectile.                \\           
    public Transform firePoint;               // Starting position of the bullet.         \\ 

    //Testing SPIRAL PATTERN
    [Range(1f, 360f)]
    public float angleStepFixed = 0f;
    //Testing INCREMENTAL PATTERN
    [Range(1f, 90f)]
    public float IncrementalAngle = 0f;

    //Testing POINTING PATTERN
    public float projectileSpread;


    [Header("Private Variables")]
    private Vector3 startPoint;                 
    private const float radius = 1f;   


    // Start is called before the first frame update
    void Start()
    {
        playerBulletPool = new Queue<GameObject>(); // creates an empty queue container
        ShotgunBulletPool = new Queue<GameObject>(); // creates an empty queue container
        factory = GameObject.FindObjectOfType<BulletFactory>();
        BuildBulletPools();
    }
    void BuildBulletPools()
    {
        for (int i = 0; i < playerBulletNumber; i++)
        {
            playerBulletPool.Enqueue(factory.CreateBullet());
             
        }
        playerBulletCount = playerBulletPool.Count;

    }


    public GameObject GetBullet(Vector3 startPosition, Vector3 targetPos, BulletType type, int _numberOfProjectiles, float angle)
    {
        GameObject bullet = null;
        switch (type)
        {
            case BulletType.SPIRAL:
                {
                    if (playerBulletPool.Count < 1)
                    {
                        playerBulletPool.Enqueue(factory.CreateBullet());
                    }
                    {
                        bullet = playerBulletPool.Dequeue();
                        // stats
                        playerBulletCount = playerBulletPool.Count;
                        activePlayerBullets++;
                    }

                }
                break;
            case BulletType.SINGLE:
                {
                    if (playerBulletPool.Count < 1)
                    {
                        playerBulletPool.Enqueue(factory.CreateBullet());
                    }
                    {
                        bullet = playerBulletPool.Dequeue();
                        // stats
                        playerBulletCount = playerBulletPool.Count;
                        activePlayerBullets++;
                    }

                }
                break;
            case BulletType.SHOTGUN:
                if (playerBulletPool.Count < 1)
                {
                    playerBulletPool.Enqueue(factory.CreateBullet());
                }
                {
                    bullet = playerBulletPool.Dequeue();
                    // stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets++;
                }
                break;
        }
               
                bullet.SetActive(true);
                bullet.transform.position = startPosition;

                switch (type)
                {
                    case BulletType.SPIRAL:
                        bullet.GetComponent<BulletBehaviour>().bulletType = type;
                        threeSixtyAngleShoot(startPosition, bullet, angle);
                        break;

                    case BulletType.SINGLE:
                        bullet.GetComponent<BulletBehaviour>().bulletType = type;
                        AimShoot(bullet);
                        break;
                    case BulletType.SHOTGUN:
                       bullet.GetComponent<BulletBehaviour>().bulletType = type;
                       SpreadShot(targetPos, bullet);
                        break;

                }
                return bullet;
        }



    public void ReturnBullet(GameObject bullet, BulletType type)
     {
            bullet.SetActive(false);

            switch (type)
            {
                case BulletType.SPIRAL:
                    playerBulletPool.Enqueue(bullet);
                    //stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets--;
                    break;
                case BulletType.SINGLE:
                    playerBulletPool.Enqueue(bullet);
                    //stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets--;
                    break;
                case BulletType.SHOTGUN:
                playerBulletPool.Enqueue(bullet);
                //stats
                playerBulletCount = playerBulletPool.Count;
                activePlayerBullets--;
                    break;
            }
     } 
    public void threeSixtyAngleShoot(Vector3 startPosition, GameObject bullet, float angle)
    {
            // Direction calculations.
            float projectileDirXPosition = startPosition.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPosition.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            // Create vectors.
            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPosition).normalized * projectileSpeed;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);

    }
    public void AimShoot(GameObject bullet)
    {
            
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
             
    }
    private void SpreadShot(Vector3 direction, GameObject bullet)
    {
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(direction * 2f, ForceMode2D.Impulse);
    }


}
