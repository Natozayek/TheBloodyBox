using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    [Header("Bullet Properties")]
    [Range(10, 1000)]
    public int playerBulletNumber = 100;
    public int playerBulletCount = 0;
    public int activePlayerBullets = 0;

    private Factory factory;
    public Queue<GameObject> playerBulletPool;
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
    Sprite[] Xprites;
    [SerializeField] private AI_Level_Manager levelManager;
    [SerializeField] public StatsVariableIncreaser VariableIncreaser;


    // Start is called before the first frame update
    void Start()
    {
        playerBulletPool = new Queue<GameObject>(); // creates an empty queue container
        factory = GameObject.FindObjectOfType<Factory>();
        levelManager = FindObjectOfType<AI_Level_Manager>();
        BuildBulletPools();
        Xprites = Resources.LoadAll<Sprite>("Sprites/Bullets");
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
                              bullet.GetComponent<BulletBehaviour>()._BulletType = type;
                              bullet.GetComponent<BulletBehaviour>()._RegularDamage = bullet.GetComponent<BulletBehaviour>()._RegularDamage * VariableIncreaser._Bullet_Damage_Multiplier;
                              bullet.GetComponent<BulletBehaviour>()._Speed = VariableIncreaser._BulletSpeed * VariableIncreaser._BulletSpeed_Multiplier;
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
                            bullet.GetComponent<BulletBehaviour>()._BulletType = type;
                            bullet.GetComponent<BulletBehaviour>()._RegularDamage = bullet.GetComponent<BulletBehaviour>()._RegularDamage * VariableIncreaser._Bullet_Damage_Multiplier;
                            bullet.GetComponent<BulletBehaviour>()._Speed = VariableIncreaser._BulletSpeed * VariableIncreaser._BulletSpeed_Multiplier;
                }
                  break;

                 case BulletType.SHOTGUN:
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
                              bullet.GetComponent<BulletBehaviour>()._BulletType = type;
                              bullet.GetComponent<BulletBehaviour>()._RegularDamage = bullet.GetComponent<BulletBehaviour>()._RegularDamage * VariableIncreaser._Bullet_Damage_Multiplier;
                              bullet.GetComponent<BulletBehaviour>()._Speed = VariableIncreaser._BulletSpeed * VariableIncreaser._BulletSpeed_Multiplier;
                }
                 break;

               case BulletType.ROCKET:
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
                       bullet.GetComponent<BulletBehaviour>()._BulletType = type;
                       bullet.GetComponent<BulletBehaviour>()._RegularDamage = bullet.GetComponent<BulletBehaviour>()._RegularDamage * VariableIncreaser._Bullet_Damage_Multiplier;
                       bullet.GetComponent<BulletBehaviour>()._Speed = VariableIncreaser._BulletSpeed * VariableIncreaser._BulletSpeed_Multiplier;
                }
                   break;
          }
               
                bullet.SetActive(true);
                bullet.transform.position = startPosition;


                switch (type)
                {
                    case BulletType.SPIRAL:
                        bullet.GetComponent<SpriteRenderer>().sprite = Xprites[3];
                        bullet.GetComponent<SpriteRenderer>().color =  Color.white;
                        bullet.GetComponent<Transform>().localScale = new Vector3(1,0.5f, 1);

                threeSixtyAngleShoot(startPosition, bullet, angle);
                        break;

                    case BulletType.SINGLE:
                        bullet.GetComponent<SpriteRenderer>().sprite = Xprites[2];
                        bullet.GetComponent<SpriteRenderer>().color = Color.white;
                        bullet.GetComponent<Transform>().localScale = new Vector3(1, 0.5f, 1);

                AimShoot(bullet, angle);
                
                break;

                    case BulletType.SHOTGUN:
                      bullet.GetComponent<SpriteRenderer>().sprite = Xprites[1];
                      bullet.GetComponent<SpriteRenderer>().color = Color.white;
                      bullet.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                SpreadShot(targetPos, bullet, angle);
                        break;


            case BulletType.ROCKET:
                bullet.GetComponent<SpriteRenderer>().sprite = Xprites[0];
                bullet.GetComponent<SpriteRenderer>().color = Color.white;
                bullet.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
                AimShoot(bullet, angle);
                break;

        }
        return bullet;
        }



    public void ReturnBullet(GameObject bullet, BulletType type)
     {
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bullet.SetActive(false);

            switch (type)
            {
                case BulletType.SPIRAL:
                {
                    playerBulletPool.Enqueue(bullet);
                    //stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets--;
                }
                break;
                case BulletType.SINGLE:
                {
                    playerBulletPool.Enqueue(bullet);
                    //stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets--;
                }
                break;
                case BulletType.SHOTGUN:
                {
                    playerBulletPool.Enqueue(bullet);
                    //stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets--;
                }
                break;

                 case BulletType.ROCKET:
                {
                    playerBulletPool.Enqueue(bullet);
                    //stats
                    playerBulletCount = playerBulletPool.Count;
                    activePlayerBullets--;
                }
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
            Vector3 projectileMoveDirection = (projectileVector - startPosition).normalized * bullet.GetComponent<BulletBehaviour>()._Speed;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);
            bullet.GetComponent<Rigidbody2D>().rotation = -angle;

    }
    public void AimShoot(GameObject bullet, float angle)
    {
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(firePoint.up * bullet.GetComponent<BulletBehaviour>()._Speed, ForceMode2D.Impulse);
            bullet.GetComponent<Rigidbody2D>().rotation = angle;
    }
    private void SpreadShot(Vector3 direction, GameObject bullet, float angle)
    {
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = Vector2.zero;
        bulletRb.AddForce(direction * bullet.GetComponent<BulletBehaviour>()._Speed, ForceMode2D.Impulse);
        bullet.GetComponent<Rigidbody2D>().rotation = angle;
    }


}
