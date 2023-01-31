using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField]private float movementSpeed;
    [SerializeField]private bool usingMobileInput = false;
    //public int numberOfProjectiles;           //Number of projectiles to shoot.  \\
    public BulletPattern DesiredPattern = BulletPattern.SINGLE;//TBM
    public FireMode FireMode = FireMode.SINGLE;//TBM
    
    private Rigidbody2D rb;
    private Camera camera;
    public Transform bulletSpawnPoint;//TBM

    [Header("Bullet Properties")] 
    public float _fireRate = 0.2f;

    [Header("Inputs")]
    [SerializeField] private KeyCode shootkey = KeyCode.Space;
    [SerializeField] private KeyCode cycleFireMode = KeyCode.Q;
    [SerializeField] private KeyCode  cyclePatterMode = KeyCode.E;


    [Header("Private Variables")]
    private float _fireTimer = 0.0f;
    float aimAngle;
    private Vector2 mousePosition;         // Helps us to aim                                      \\
    private Vector2 movementDirection;
    private bool _bursting = false;//TBM
    private bool _PressingShootKey = false;//TBM
   // private ScoreManager scoreManager;
    private BulletManager bulletManager;//TBM
    //private AudioSource _audioSource = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _fireRate = 0.25f;// TBM

        // scoreManager = FindObjectOfType<ScoreManager>();
        bulletManager = FindObjectOfType<BulletManager>();
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
        
        Move();
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
    private void CyclePatternsType() => DesiredPattern = ((int)DesiredPattern < 2) ? DesiredPattern + 1 : 0;
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
    
    public void Move()
    {
        //float YclampedPosition = Mathf.Clamp(transform.position.y, boundary.min, boundary.max);
        //float XclampedPosition = Mathf.Clamp(transform.position.x, boundary.min, boundary.max);
        //transform.position = new Vector2(XclampedPosition, YclampedPosition);
    }

    #region TO TO BE MOVED TO A NEW SCRIPT
    void Fire()
    {
        int numofBullet = 5;
       // _canFire = Time.time + _fireRate;

        switch (DesiredPattern)
        {
            case BulletPattern.SPIRAL:
                {
                    float angleStep = 360 / numofBullet;
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float angle = aimAngle;
                    var centerBullet = bulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SINGLE, 1, angle);
                    centerBullet.GetComponent<BulletBehaviour>().bulletType = BulletType.SINGLE;
                    for (int i = 0; i <numofBullet ; i++)
                    {
                        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SPIRAL, i, angle);
                        bullet.GetComponent<BulletBehaviour>().bulletType = BulletType.SPIRAL;
                        angle += angleStep;
                    }
                 
                }
                bulletManager.angle = 0.0f;
                
                break;
            case BulletPattern.INCREMENTAL:
                {
                    var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, new Vector3(0f,0f,0f), BulletType.INCREMENTAL, 0, 0);
                }
                break;
            case BulletPattern.SINGLE:
                {
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float angle = aimAngle;
                    var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SINGLE, 1, angle);
                    bullet.GetComponent<BulletBehaviour>().bulletType = BulletType.SINGLE;
                }
                break;
            case BulletPattern.SHOTGUN:
                {
                    Vector3 _targetPosition, _targetPosition2, _targetPosition3;
                    Vector3 mouseDirection = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90f;
                    float angle = aimAngle;
                    var direction = Quaternion.Euler(0, 0, -30f) * (mouseDirection - transform.position); //-30 
                    var direction2 = Quaternion.Euler(0, 0, 0f) * (mouseDirection - transform.position); //-0
                    var direction3 = Quaternion.Euler(0, 0, 30f) * (mouseDirection - transform.position); //30 

                    var bullet = bulletManager.GetBullet(bulletSpawnPoint.position,direction, BulletType.SHOTGUN, 1, angle);
                    var bullet2 = bulletManager.GetBullet(bulletSpawnPoint.position, direction2, BulletType.SHOTGUN, 1, angle);
                    var bullet3 = bulletManager.GetBullet(bulletSpawnPoint.position, direction3, BulletType.SHOTGUN, 1, angle);

                    bullet.GetComponent<BulletBehaviour>().bulletType = BulletType.SHOTGUN;
                    bullet2.GetComponent<BulletBehaviour>().bulletType = BulletType.SHOTGUN;
                    bullet3.GetComponent<BulletBehaviour>().bulletType = BulletType.SHOTGUN;


                }

                break;
            default:
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


    /*
     * 
     * 
     * ﻿
[Header("Inputs")]
[SerializeField] private KeyCode shootkey = KeyCode.Mouse®;
[Header("Weapon Logic")]
[SerializeField] private FireMode fireMode = FireMode.Semi; [SerializeField] private float fireRate = 0.88f;
[Header("Weapon Sources")]
[SerializeField] private AudioClip shootSound = null; [SerializeField] private Animator _weaponAnimator = null;
private float _fireTimer
0.0f;
private bool _shootInput = false;

private void Awake()
{
}
_audioSource = GetComponent<AudioSource>();
     * private bool CanShoot()
if (_fireTimer < _fireRate) return false;
if (_bursting) return false;
return true;
}
void Update()
switch (_fireMode)
{
case FireMode.Auto:
_shootInput = Input.GetKey(_shootkey); break;
default:
_shootInput = Input.GetKeyDown(_shootkey); break;
// Increase Fire Timer
if (_fireTimer < _fireRate + 1.0f)
_fireTimer += Time.deltaTime;
// Shoot
if (_shootInput)
if (CanShoot())
{
// Shoot
Shoot();
// Reset Fire Timer
_fireTimer = 0.0f;
// Burst
if (_fireMode == FireMode.Burst)
{
_bursting = true;
StartCoroutine (BurstFire());
}
// Fire Mode
if (Input.GetKeyDown(_cycleFireModeKey))
CycelFireMode();
// Shoot Weapon Logic
private void Shoot()
{
_audioSource.PlayOneShot (_shootSound);
_weaponAnimator.play("UMPFire", -1, 0);
Debug.Log("Bang!");
}
     * 
     */
}



