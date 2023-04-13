using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField]public float movementSpeed;
    [Range(0f, 0.5f)]
    [SerializeField] private float maxStregth;
    [SerializeField]private bool usingMobileInput = false;

    public BulletType DesiredPattern = BulletType.SINGLE;//TBM
    public FireMode FireMode = FireMode.SINGLE;//TBM
    [SerializeField] int numberOfProjectiles = 3;
    [SerializeField] bool isJoystickFire = false;
    [SerializeField] bool isJoystick;
    [SerializeField] GameObject _PlayerBody, _PlayerWeapon, _DeathAnimation;

    [Header("HealthSystem")]
    public HealthBarController health;


    [Header("Bullet Properties")]
    [SerializeField] private float _fireRate;
    private float _autoFireRate = 0.1f;
    private float _customFireRate = 1.0f;

    [Header("Inputs")]
    [SerializeField] private KeyCode shootkey = KeyCode.Space;
    [SerializeField] private KeyCode cycleFireMode = KeyCode.Q;
    [SerializeField] private KeyCode cyclePatterMode = KeyCode.E;
    [SerializeField] private FloatingJoystick _MovementJoystick;
    [SerializeField] private FloatingJoystick _RotationJoystick;

    [Header("Private Variables")]
    SpawnManager _SpawnManager;
    BulletManager _BulletManager;
    private Transform bulletSpawnPoint;
    private float _fireTimer = 0.0f;
    private Vector2 mousePosition;
    private Vector2 _JoystickPosition;// Helps us to aim 
    public Vector2 movementDirection;
    private bool _bursting = false;
    private bool _PressingShootKey = false;
   
    public Rigidbody2D rb;
    private Camera camera;
    private bool DoubleShootOn = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject _Muzzle;
    [SerializeField] private GameObject _GameOverScene;
  

    void Start()
    {
        _BulletManager = FindObjectOfType<BulletManager>();
        _SpawnManager = FindObjectOfType<SpawnManager>();
        health = FindObjectOfType<HealthBarController>().GetComponent<HealthBarController>();
        bulletSpawnPoint = GameObject.Find("firePoint").transform;
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        animator = GetComponentInChildren<Animator>();

        // Platform Detection for input
        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;
    }
    void Update()
  {
   
        _JoystickPosition.x = _RotationJoystick.Direction.x;
        _JoystickPosition.y = _RotationJoystick.Direction.y;

        if (isJoystick || usingMobileInput)
        {
            MobileInput();
        }
        else
        {
            ConventionalInput();
        }


        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
     
        if (_RotationJoystick.Horizontal >= 0.5f || _RotationJoystick.Horizontal <= -0.5f 
           || _RotationJoystick.Vertical >= 0.5f || _RotationJoystick.Vertical <= -0.5f)
        {
            isJoystickFire = true;
        }
       if(_RotationJoystick.Horizontal == 0
           || _RotationJoystick.Vertical == 0.0f)
        {
            isJoystickFire = false;
        }

        switch (FireMode) // TOOLS - TBM
        {
            case FireMode.AUTO:
                _PressingShootKey = Input.GetKey(shootkey);
                break;
            default:
                _PressingShootKey = Input.GetKeyDown(shootkey);
                break;
        }

        if(FireMode == FireMode.AUTO)
        {
            _fireRate = _autoFireRate;
        }
        if(FireMode == FireMode.SINGLE)
        {
            _fireRate = _customFireRate;
        }
        
       
        // Increase Fire Timer - TBM
        if (_fireTimer < _fireRate + 0.5f)
            _fireTimer += Time.deltaTime;

        if (_PressingShootKey || isJoystickFire)
        {
            if(CanShoot() == true && FireMode != FireMode.AUTO || CanShoot() == true && FireMode == FireMode.AUTO)
            {
                

                if (DoubleShootOn)
                {
                    Fire();
                    StartCoroutine(DoubleShotOn());
                }
                else
                {
                    Fire();
                }
     

                _fireTimer = 0.0f;

                if (FireMode == FireMode.BURST)
                {
                    _bursting = true;
                    StartCoroutine(BurstShot());
                }
            }
            else
            {
                //Do nothing
            }
          
        }

        
        

        // to be moved
        if (Input.GetKeyDown(cycleFireMode))
        {
            CycleFireMode();
        }
        if (Input.GetKeyDown(cyclePatterMode))
        {
            CyclePatternsType();
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(1.0f - maxStregth);
            //TODO: Play the hurt sound
            if (health.value <= 0)
            {

                DeathSequence();

            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("CorrosiveFloor"))
        {
            health.TakeDamage(0.5f - maxStregth);
            //TODO: Play the hurt sound
            if (health.value <= 0)
            {

                DeathSequence();

            }
        }
    }

    private void DeathSequence()
    {
        _SpawnManager.GameOverOn = true;
        _PlayerBody.SetActive(false);
        _PlayerWeapon.SetActive(false);
        _DeathAnimation.SetActive(true);
        // Play death sound
        StartCoroutine(TurnOnGameOverScene());

    }
    public void ResetVariables()
    {
        movementSpeed = 12;
        maxStregth = 0;
        _fireRate = 1;
        DesiredPattern = BulletType.SINGLE;
        FireMode = FireMode.SINGLE;
    }



    #region IENUMERATORS FOR MUZZLE AND DEATH ANIMATIONS
    private IEnumerator TurnOfMuzzle()
    {
        yield return new WaitForSeconds(_fireRate -0.8f);
        _Muzzle.gameObject.SetActive(false);
    }

    private IEnumerator TurnOnGameOverScene()
    {
        yield return new WaitForSeconds(1.5f);
        _GameOverScene.SetActive(true);
        _DeathAnimation.SetActive(false);
        _PlayerBody.SetActive(true);
        _PlayerWeapon.SetActive(true);
    }

    #endregion

    #region Cylce Functions - Change FireMode or BulletType
    public void CycleFireMode() => FireMode = ((int)FireMode < 2) ? FireMode +1 : 0;
    public void CyclePatternsType() => DesiredPattern = ((int)DesiredPattern < 3) ? DesiredPattern + 1 : 0;
    #endregion

    #region Input Mode - Mobile or Conventional
    private void FixedUpdate()
    {
        if(isJoystick || usingMobileInput)
        {
            rb.MovePosition(rb.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
            float aimAngle = Mathf.Atan2(_JoystickPosition.y, _JoystickPosition.x) * Mathf.Rad2Deg - 90f; ;
            rb.rotation = aimAngle;// 
        }
        else
        {
            rb.MovePosition(rb.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
            Vector2 direction = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;// 
        }

    }
    public void MobileInput() 
    {

        movementDirection.x = _MovementJoystick.Horizontal;
        movementDirection.y = _MovementJoystick.Vertical;
    }
    public void ConventionalInput()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal"); // CHANGE FOR JOYSTICK.HORIZONTAL
        movementDirection.y = Input.GetAxisRaw("Vertical");

    }
    #endregion

    #region Fire Methods - Double and Bursts Coroutines
    void Fire()
    {
        int numofBullet = 10;
     

        switch (DesiredPattern)
        {
         
            case BulletType.SINGLE :
                {
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float JaimAngle = Mathf.Atan2(_JoystickPosition.y, _JoystickPosition.x) * Mathf.Rad2Deg - 90f;
                    float angle = 0;
                    if (isJoystick || usingMobileInput)
                    {
                         angle = JaimAngle;
                    }
                    else
                    {
                        angle = aimAngle;
                    }
                      
                    var bullet = _BulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SINGLE, 1, angle);
                    _Muzzle.gameObject.SetActive(true);
                    StartCoroutine(TurnOfMuzzle());

                }
                break;
            case BulletType.SHOTGUN:
                {
                    float angleSpread = -45f;
                    Vector3 mouseDirection = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90f;
                    float JaimAngle = Mathf.Atan2(_JoystickPosition.y, _JoystickPosition.x) * Mathf.Rad2Deg - 90f;
                  //  float JaimAngle = Mathf.Atan2(_JoystickPosition.y, _JoystickPosition.x) * Mathf.Rad2Deg - 90f; ;
                    float angle = 0;
                    if (isJoystick || usingMobileInput)
                    {
                        angle = JaimAngle;
                    }
                    else
                    {
                        angle = aimAngle;
                    }


                    //var direction2 = Quaternion.Euler(0, 0, 0f) * (mouseDirection - transform.position); //-0
                    //var direction3 = Quaternion.Euler(0, 0, 30f) * (mouseDirection - transform.position); //30 

                    Vector2 _Direction;

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                      
                      
                        if (isJoystick || usingMobileInput)
                        {
                            Vector3 jpos = _JoystickPosition;
                            _Direction = Quaternion.Euler(0, 0, angleSpread) * jpos; /** (jpos - transform.position)*/;
                        }
                        else
                        {
                             _Direction = Quaternion.Euler(0, 0, angleSpread) * (mouseDirection - transform.position);
                        }//-30 
                        var bullet = _BulletManager.GetBullet(bulletSpawnPoint.position, _Direction, BulletType.SHOTGUN, 1, angleSpread + angle);
                        angleSpread += 45f;
                    }

                    //var bullet2 = bulletManager.GetBullet(bulletSpawnPoint.position, direction2, BulletType.SHOTGUN, 1, angle);
                    //var bullet3 = bulletManager.GetBullet(bulletSpawnPoint.position, direction3, BulletType.SHOTGUN, 1, angle);
                    _Muzzle.gameObject.SetActive(true);
                    StartCoroutine(TurnOfMuzzle());

                }

                break;

            case BulletType.SPIRAL:
                {
                    float angleStep = 360 / numofBullet;
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float JaimAngle = Mathf.Atan2(_JoystickPosition.y, _JoystickPosition.x) * Mathf.Rad2Deg - 90f;
                    float _Angle = 0;
                    if (isJoystick)
                    {
                        _Angle = JaimAngle;
                    }
                    else
                    {
                        _Angle = aimAngle;
                    }

                    for (int i = 0; i < numofBullet; i++)
                    {
                        var bullet = _BulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.SPIRAL, 1, _Angle);
                        _Angle += angleStep;
                    }

                    _Muzzle.gameObject.SetActive(true);
                    StartCoroutine(TurnOfMuzzle());
                }
                _BulletManager.angle = 0.0f;
                break;


            case BulletType.ROCKET:
                {
                    Vector2 direction = mousePosition - rb.position;
                    float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    float JaimAngle = Mathf.Atan2(_JoystickPosition.y, _JoystickPosition.x) * Mathf.Rad2Deg - 90f;
                    float _Angle = 0;
                    if (isJoystick)
                    {
                        _Angle = JaimAngle;
                    }
                    else
                    {
                        _Angle = aimAngle;
                    }
                    var bullet = _BulletManager.GetBullet(bulletSpawnPoint.position, direction, BulletType.ROCKET, 1, _Angle);
                    _Muzzle.gameObject.SetActive(true);
                    StartCoroutine(TurnOfMuzzle());

                }
                break;

        }


    }
    private bool CanShoot()
    {
        if (_fireTimer < _fireRate) { return false; }
        else if (_bursting) { return false; }
        else if (_fireTimer > _fireRate) { return true; }
        else
        {
            return false;
        }
    }
    private IEnumerator DoubleShotOn()
    {
        yield return new WaitForSeconds(0.5f);
        Fire();
    }
    private IEnumerator BurstShot()
    {
        yield return new WaitForSeconds(_fireRate - 0.8f);
        Fire();
        yield return new WaitForSeconds(_fireRate -0.8f);
        Fire();
        yield return new WaitForSeconds(_fireRate - 0.8f);
        _bursting = false;
    }
    #endregion
    
    #region Power ups - Stats modifiers


    public void increaseMAXHP()
    {
        health.IncreasMaxHP();
    }
    public void SpeedUP()
    {
        movementSpeed += movementSpeed * 0.1f;
    }
    public void FireRateUP()
    {
        _customFireRate -= 0.1f;
    }
    public void ChangeBulletPattern()
    {
          if (DesiredPattern == BulletType.SINGLE)
          {
            CyclePatternsType();
            return;
          }
          if (DesiredPattern == BulletType.SHOTGUN)
        {
            CyclePatternsType();
            return;
        }
        if (DesiredPattern == BulletType.SPIRAL)
        {
            return;
        }
    
    }

    public void DoubleShotActive()
    {
        if (!DoubleShootOn)
        {
            DoubleShootOn = true;
        }
    }
    public void SetBurstShotActive()
    {
        FireMode = FireMode.BURST;
    }
    public void IncreaseStrength()
    {
        if(maxStregth < 0.5f)
        {
            maxStregth += 0.1f;
        }
    }
    public void SetAutomaticShot()
    {
        FireMode = FireMode.AUTO;
    }

    public void SetRocketShot()
    {
        DesiredPattern = BulletType.ROCKET;
    }
    #endregion
    #region Tools
    void WrapAround(Vector3 vector, float min, float max)
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
        vector.z = WrapAroundFloat(vector.z, min, max);
    }
    public float WrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
            value = min;
        else if (value < min)
            value = max;
        return value;
    }
    #endregion
}



