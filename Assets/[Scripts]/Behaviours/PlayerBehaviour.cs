﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField]private float movementSpeed;
    [SerializeField]private bool usingMobileInput = false;
    public BulletType DesiredPattern = BulletType.SINGLE;//TBM
    public FireMode FireMode = FireMode.SINGLE;//TBM
    [SerializeField] int numberOfProjectiles = 5;

    [Header("HealthSystem")]
    public HealthBarController health;


    [Header("Bullet Properties")] 
    public float _fireRate = 0.2f;

    [Header("Inputs")]
    [SerializeField] private KeyCode shootkey = KeyCode.Space;
    [SerializeField] private KeyCode cycleFireMode = KeyCode.Q;
    [SerializeField] private KeyCode  cyclePatterMode = KeyCode.E;


    [Header("Private Variables")]
    private Transform bulletSpawnPoint;//TBM
    private float _fireTimer = 0.0f;
    private Vector2 mousePosition;         // Helps us to aim                                      \\
    private Vector2 movementDirection;
    private bool _bursting = false;//TBM
    private bool _PressingShootKey = false;//TBM
    // private ScoreManager scoreManager;
    private BulletManager bulletManager;//TBM
    //private AudioSource _audioSource = null;
    private Rigidbody2D rb;
    private Camera camera;
    [SerializeField] BulletBehaviour[] bullets;

    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        // scoreManager = FindObjectOfType<ScoreManager>();
        health = FindObjectOfType<HealthBarController>().GetComponent<HealthBarController>();
        bulletSpawnPoint = GameObject.Find("firePoint").transform;
        rb = GetComponent<Rigidbody2D>();
        _fireRate = 0.25f;// TBM

        bullets = FindObjectsOfType<BulletBehaviour>();

   
        
        camera = Camera.main;

        // Platform Detection for input
        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;
    }
    private bool CanShoot()//TBM
    {
        if (_fireTimer < _fireRate) return false;
        if (_bursting) return false;
        return true;
    }


// Update is called once per frame
void Update()
  {
        if (usingMobileInput)
        {
            MobileInput();
        }
        else
        {
            ConventionalInput();
        }
        
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        switch (FireMode) // TOOLS - TBM
        {
            case FireMode.AUTO:
                _PressingShootKey = Input.GetKey(shootkey);
                break;
            default:
                _PressingShootKey = Input.GetKeyDown(shootkey);
                break;
        }

        // Increase Fire Timer - TBM
        if (_fireTimer < _fireRate + 1.0f)
            _fireTimer += Time.deltaTime;

        if (_PressingShootKey)
        {
            if(CanShoot())
            {
                Fire();

                _fireTimer = 0.0f;

                if (FireMode == FireMode.BURST)
                {
                    _bursting = true;
                    StartCoroutine(BurstShot());
                }
            }
            
        }

        // to be moved
        if(Input.GetKeyDown(cycleFireMode))
        {
            CycleFireMode();
        }
        if (Input.GetKeyDown(cyclePatterMode))
        {
            CyclePatternsType();
        }


    }
    #region TO BE MOVED TO A NEW SCRIPT
    private void CycleFireMode() => FireMode = ((int)FireMode < 2) ? FireMode +1 : 0;
    public void CyclePatternsType() => DesiredPattern = ((int)DesiredPattern < 2) ? DesiredPattern + 1 : 0;
    #endregion

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
        Vector2 direction = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;// 
    }
    public void MobileInput()
    {
        foreach (var touch in Input.touches)
        {
            var destination = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * movementSpeed);
        }
    }
    public void ConventionalInput()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        
    }
    

    #region TO TO BE MOVED TO A NEW SCRIPT
    void Fire()
    {
        int numofBullet = 10;
       // _canFire = Time.time + _fireRate;

        switch (DesiredPattern)
        {
         
            case BulletType.SINGLE:
                {
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float angle = aimAngle;
                    var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SINGLE, 1, angle);
              
                }
                break;
            case BulletType.SHOTGUN:
                {
                    float angleSpread = -60f;
                    Vector3 mouseDirection = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90f;
                
                  
                    var direction2 = Quaternion.Euler(0, 0, 0f) * (mouseDirection - transform.position); //-0
                    var direction3 = Quaternion.Euler(0, 0, 30f) * (mouseDirection - transform.position); //30 


                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                      
                        var direction = Quaternion.Euler(0, 0, angleSpread) * (mouseDirection - transform.position); //-30 
                        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SHOTGUN, 1, angleSpread + aimAngle);
                        angleSpread += 30f;
                    }
                  
                    //var bullet2 = bulletManager.GetBullet(bulletSpawnPoint.position, direction2, BulletType.SHOTGUN, 1, angle);
                    //var bullet3 = bulletManager.GetBullet(bulletSpawnPoint.position, direction3, BulletType.SHOTGUN, 1, angle);


                }

                break;

            case BulletType.SPIRAL:
                {
                    float angleStep = 360 / numofBullet;
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float angle = aimAngle;

                    for (int i = 0; i < numofBullet; i++)
                    {
                        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SPIRAL, 1, angle);
                        angle += angleStep;
                    }

                }
                bulletManager.angle = 0.0f;
                break;

        }


    }
    private IEnumerator BurstShot()
    {
        yield return new WaitForSeconds(_fireRate);
        Fire();
        yield return new WaitForSeconds(_fireRate);
        Fire();
        yield return new WaitForSeconds(_fireRate);
        _bursting = false;
    }
    #endregion
    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(1);
            if (health.value <= 0)
            {
                SceneManager.LoadScene("Waves");
                //soundManager.PlaySoundFX(SoundFX.HURT, SoundChannel.PLAYER_HURT_FX);

                //TODO: Play the hurt sound
            }
        }
    }

    public void increaseBulletPower()
    {
        BulletBehaviour.instance.damage = BulletBehaviour.instance.damage * 1.02f;
    }
    public void increaseBulletSpeed()
    {
        BulletBehaviour.instance.speed += 0.25f;
    }
    public void increaseMAXHP()
    {
        health.IncreasMaxHP();
    }
    public void SpeedUP()
    {
        Debug.Log("SpeedUP");
    }
    public void FireRateUP()
    {
        Debug.Log("FireRateUP");
    }

    public void ChangeBulletPattern()
    {
        Debug.Log("ChangeBulletPattern");
        //  CyclePatternsType(); 
    }
    public void DoubleShotActive()
    {
        Debug.Log("DoubleShotActive");
    }
    public void SetBurstShotActive()
    {
        Debug.Log("BurstShotActive");
        //   CycleFireMode();
    }
    public void IncreaseStrength()
    {
        Debug.Log("IncreaseStrength");
        //   CycleFireMode();
    }

    public void SetAutomaticShot()
    {
        Debug.Log("SetAutomaticShot");
    }
}



