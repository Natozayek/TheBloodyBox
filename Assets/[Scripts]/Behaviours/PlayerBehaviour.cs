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
    [SerializeField]private float movementSpeed;
    [Range(0f, 0.5f)]
    [SerializeField] private float maxStregth;
    [SerializeField]private bool usingMobileInput = false;
    public BulletType DesiredPattern = BulletType.SINGLE;//TBM
    public FireMode FireMode = FireMode.SINGLE;//TBM
    [SerializeField] int numberOfProjectiles = 5;
    

    [Header("HealthSystem")]
    public HealthBarController health;


    [Header("Bullet Properties")]
    public float _fireRate;
    private float _autoFireRate = 0.1f;
    private float _customFireRate = 1.0f;

    [Header("Inputs")]
    [SerializeField] private KeyCode shootkey = KeyCode.Space;
    //[SerializeField] private KeyCode cycleFireMode = KeyCode.Q;
    //[SerializeField] private KeyCode  cyclePatterMode = KeyCode.E;


    [Header("Private Variables")]
    private Transform bulletSpawnPoint;
    private float _fireTimer = 0.0f;
    private Vector2 mousePosition;         // Helps us to aim 
    private Vector2 movementDirection;
    private bool _bursting = false;
    private bool _PressingShootKey = false;
    // private ScoreManager scoreManager;
    private BulletManager bulletManager;
    //private AudioSource _audioSource = null;
    private Rigidbody2D rb;
    private Camera camera;
    [SerializeField]private bool DoubleShootOn = false;

    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        // scoreManager = FindObjectOfType<ScoreManager>();
        health = FindObjectOfType<HealthBarController>().GetComponent<HealthBarController>();
        bulletSpawnPoint = GameObject.Find("firePoint").transform;
        rb = GetComponent<Rigidbody2D>();
       // _fireRate = 0.15f;
        camera = Camera.main;


        // Platform Detection for input
        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;
    }
    private bool CanShoot()//TBM
    {
        if (_fireTimer < _fireRate)
        { return false;
            Debug.Log("Cant fire");
        }

       else if (_bursting)
        {
            Debug.Log("Cant fire");
            return false;
        }
        else if(_fireTimer > _fireRate)
        {
            Debug.Log("Can fire");
            return true;
        }
        else
        {
            Debug.Log("Cant fire");
            return false;
        }
        
        
    }

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

        if(FireMode == FireMode.AUTO)
        {
            _fireRate = _autoFireRate;
        }
        if(FireMode == FireMode.SINGLE)
        {
            _fireRate = _customFireRate;
        }
        
       
        // Increase Fire Timer - TBM
        if (_fireTimer < _fireRate + 1f)
            _fireTimer += Time.deltaTime;

        if (_PressingShootKey)
        {
            if(CanShoot() == true && FireMode != FireMode.AUTO || CanShoot() == true && FireMode == FireMode.AUTO)
            {
                if(DoubleShootOn)
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
                Debug.Log("CantShoot");
            }
            
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
    private IEnumerator DoubleShotOn()
    {
        yield return new WaitForSeconds(0.5f);
        Fire();
    }
    private IEnumerator BurstShot()
    {
        yield return new WaitForSeconds(_fireRate - 0.5f);
        Fire();
        yield return new WaitForSeconds(_fireRate -0.5f);
        Fire();
        yield return new WaitForSeconds(_fireRate - 0.5f);
        _bursting = false;
    }
    #endregion
    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(1.0f - maxStregth);
            //TODO: Play the hurt sound
            if (health.value <= 0)
            {
                //Set active Game over Menu, Give restart or return to main options
               
            }
        }
    }

    public void increaseBulletPower()
    {

            
        for (int i = 0; i < bulletManager.playerBulletPool.Count; i++)
        {
            if (bulletManager.playerBulletPool.ElementAt(i).gameObject != null)
            {
                bulletManager.playerBulletPool.ElementAt(i).gameObject.GetComponent<BulletBehaviour>().damage += bulletManager.playerBulletPool.ElementAt(i).gameObject.GetComponent<BulletBehaviour>().damage * 0.15f;

              

            }
        }
    }
    public void increaseBulletSpeed()
    {
        for (int i = 0; i < bulletManager.playerBulletPool.Count; i++)
        {
            if (bulletManager.playerBulletPool.ElementAt(i).gameObject != null)
            {
                bulletManager.playerBulletPool.ElementAt(i).gameObject.GetComponent<BulletBehaviour>().speed += bulletManager.playerBulletPool.ElementAt(i).gameObject.GetComponent<BulletBehaviour>().speed * 0.05f;

                Debug.Log(bulletManager.playerBulletPool.ElementAt(i).gameObject.GetComponent<BulletBehaviour>().speed);
            }
        }
    }
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
            return ;
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
}



