using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    public Boundary boundary;
    public float movementSpeed = 2.0f;
    public float verticalSpeed = 10.0f;
    public bool usingMobileInput = false;
    public int numberOfProjectiles;           //Number of projectiles to shoot.  \\
    public BulletPattern DesiredPattern = BulletPattern.SINGLE;
    public FireMode FireMode = FireMode.SINGLE;
    
    public Rigidbody2D rb;
    public Camera camera;
    public Transform bulletSpawnPoint;

    [Header("Bullet Properties")] 
    public float _fireRate = 0.2f;
    //private float _canFire = -1f;

    [Header("Inputs")]
    [SerializeField] private KeyCode shootkey = KeyCode.Space;
    [SerializeField] private KeyCode cycleFireMode = KeyCode.Q;
    [SerializeField] private KeyCode  cyclePatterMode = KeyCode.E;


    [Header("Private Variables")]
    private float _fireTimer = 0.0f;
    float aimAngle;
    private Vector2 mousePosition;         // Helps us to aim                                      \\
    private Vector2 movementDirection;
    private bool _bursting = false;
    private bool _PressingShootKey = false;
   // private ScoreManager scoreManager;
    private BulletManager bulletManager;
    //private AudioSource _audioSource = null;

    void Start()
    {
        movementSpeed = 2.0f;
        _fireRate = 0.25f;
       // scoreManager = FindObjectOfType<ScoreManager>();
        bulletManager = FindObjectOfType<BulletManager>();
       // transform.position = new Vector2(0.0f, horizontalPos);
        camera = Camera.main;
        // Platform Detection for input
        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;
    }
    private bool CanShoot()
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

        switch (FireMode)
        {
            case FireMode.AUTO:
                _PressingShootKey = Input.GetKey(shootkey);
                break;
            default:
                _PressingShootKey = Input.GetKeyDown(shootkey);
                break;
        }

        // Increase Fire Timer
        if (_fireTimer < _fireRate + 1.0f)
            _fireTimer += Time.deltaTime;

        if (_PressingShootKey)
        {
            if(CanShoot())
            {
                SingleShot();

                _fireTimer = 0.0f;

                if (FireMode == FireMode.BURST)
                {
                    _bursting = true;
                    StartCoroutine(BurstShot());
                }
            }
            
        }

        if(Input.GetKeyDown(cycleFireMode))
        {
            CycleFireMode();
        }
        if (Input.GetKeyDown(cyclePatterMode))
        {
            CyclePatternsType();
        }
        //For Score system
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    scoreManager.AddPoints(10);
        //}

    }

    private void CycleFireMode() => FireMode = ((int)FireMode < 2) ? FireMode +1 : 0;
    private void CyclePatternsType() => DesiredPattern = ((int)DesiredPattern < 2) ? DesiredPattern + 1 : 0;


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
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * verticalSpeed);
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

    void SingleShot()
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
                    var centerBullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.SINGLE, 1, angle);
                    for (int i = 0; i <numofBullet ; i++)
                    {
                        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.SPIRAL, i, angle);
                        angle += angleStep;
                    }
                 
                }
                bulletManager.angle = 0.0f;
                
                break;
            case BulletPattern.INCREMENTAL:
                {
                    var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.INCREMENTAL, numberOfProjectiles, 0);
                }
                break;
            case BulletPattern.SINGLE:
                {
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float angle = aimAngle;
                    var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.SINGLE, 1, angle);

                }
                break;
            case BulletPattern.SHOTGUN:
                break;
            default:
                break;
        }
       

    }
    private IEnumerator BurstShot()
    {
        yield return new WaitForSeconds(_fireRate);
        SingleShot();
        yield return new WaitForSeconds(_fireRate);
        SingleShot();
        yield return new WaitForSeconds(_fireRate);
        _bursting = false;
    }


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



