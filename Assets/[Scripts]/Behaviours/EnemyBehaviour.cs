using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Properties")]
    [SerializeField] public  EnemyHealthBarController _HealthBarController;
    [SerializeField] public float _Health, _MaxtHealth = 0;
    [SerializeField] public float _Speed = 1f;
    [SerializeField] public EnemyType _EnemyType;
    [SerializeField] GameObject _StainPrefab, _CorrosiveFloor;
    [SerializeField] GameObject _Blood_VFX_Prefab;
    [SerializeField] public GameObject _BasicAnimator, _TankAnimator, _ExplodingAnimator, HealthBar;
    public bool isChasing;
    public bool isEvading;
   
    //Private properties
    EnemyManager _EnemyManager;
    SpawnManager _SpawnManager;
    AI_Level_Manager _LevelController;
    AI_Controller _Controller;

    Vector3 _Position;
    Vector3 _Velocity;
    Vector3 _Acceleration;
    Vector3 _Wander_Target;
    Vector2 Direction;

    Rigidbody2D rb;
    Transform _Target;
    public float _Timer;
    public static EnemyBehaviour Instance;

    private void Awake()
    {
        Instance = this;
        isChasing = true;
        isEvading = false;
        _LevelController = FindObjectOfType<AI_Level_Manager>();
        _Controller = FindObjectOfType<AI_Controller>();
    }
    private void OnEnable()
    {
        _LevelController.ListOfEnemies.Add(this.gameObject.GetComponent<EnemyBehaviour>());
    }
    private void OnDisable()
    {
        _LevelController.ListOfEnemies.Remove(this.gameObject.GetComponent<EnemyBehaviour>());
    }
    void Start()
    {
        //Enemy properties initialization
        _EnemyManager = FindObjectOfType<EnemyManager>();
        _SpawnManager = FindObjectOfType<SpawnManager>();
        _Health = _MaxtHealth;
        _Target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        _Position = transform.position;

        //Set target to be chased
        if (_Target != null)
        {
            rb.velocity = new Vector2(Direction.x, Direction.y) * _Speed;
            _Velocity = rb.velocity;
        }
    }
    void Update()
    {
        
        RotateSprite();

        if (_SpawnManager.intermissionOn || _SpawnManager.GameOverOn)
        {
            DeathSequence();
        }
        if (_Target != null)
        {
            Vector3 tempDirection = (_Target.position - transform.position).normalized;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            Direction = tempDirection;
        }
        if (isChasing)
        {
            switch(_EnemyType)
            {
                case EnemyType.BASIC:
                    AI_Movement();
                    break;
                case EnemyType.TANK:
                    AI_Movement();
                    break;
                case EnemyType.EXPLOSIVE:
                    AI_Movement();
                    break;
            }
           
        }

        _Timer = _Timer + Time.deltaTime;

        if (_Timer >= 3 && !isChasing)
        {
            isChasing = true;
            _Timer = 0;
        }
        if (_Timer >= 3 && isEvading)
        {
            isChasing = true;
            isEvading = false;
            _Timer = 0;
        }
        _Position.z = 0;

    }

    private void RotateSprite()
    {
        if (Direction.x >= 0.1)
        {
            switch (_EnemyType)
            {
                case EnemyType.BASIC:
                    {
                        _BasicAnimator.gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(1.5f, 1.5f, 0);
                        break;
                    }
                case EnemyType.TANK:
                    {

                        _TankAnimator.gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(3f, 3f, 0);
                        break;
                    }
                case EnemyType.EXPLOSIVE:
                    {

                        _ExplodingAnimator.gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(2.0f, 2.0f, 0);
                        break;
                    }
            }

        }
        if (Direction.x <= 0.1)
        {
            switch (_EnemyType)
            {
                case EnemyType.BASIC:
                    {
                        _BasicAnimator.gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(-1.5f, 1.5f, 0);
                        break;
                    }
                case EnemyType.TANK:
                    {

                        _TankAnimator.gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(-3f, 3f, 0);
                        break;
                    }
                case EnemyType.EXPLOSIVE:
                    {

                        _ExplodingAnimator.gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(-2.0f, 2.0f, 0);
                        break;
                    }
            }

        }
    }

    private void AI_Movement()
    {
        if (_Target != null)//is not evading)
        {
            rb.velocity = new Vector2(Direction.x, Direction.y) *  _Speed;
            _Velocity = rb.velocity; 

        }

        _Position = transform.position;
        _Acceleration = Combine();
        _Acceleration = Vector3.ClampMagnitude(_Acceleration, _Controller._Max_Acceleration);
        _Velocity = _Velocity + _Acceleration * Time.deltaTime;
        _Velocity = Vector3.ClampMagnitude(_Velocity, _Controller._Max_Velocity);
        _Position = _Position + _Velocity * Time.deltaTime;
        transform.position = _Position;
    }
    public void TakeDamage(float damageAmount)
    {
         _Health -= damageAmount;


        if (_Health <= 0)
        {
            rb.velocity = Vector3.zero;
            _Health = 0;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            DeathSequence();

        }
    }

    //To Trigger Animations
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            switch (_EnemyType)
            {
                case EnemyType.BASIC:
                    {
                      var Animator =  _BasicAnimator.gameObject.GetComponentInChildren<Animator>();
                        Animator.SetTrigger("attack");
                        break;
                    }
                case EnemyType.TANK:
                    {
                      
                        var Animator = _TankAnimator.gameObject.GetComponentInChildren<Animator>();
                        Animator.SetTrigger("attack");
                        break;
                    }
                case EnemyType.EXPLOSIVE:
                    {

                        var Animator = _ExplodingAnimator.gameObject.GetComponentInChildren<Animator>();
                        Animator.SetTrigger("attack");
                        DeathSequence();
                        break;
                    }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Bullet"))  
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.GetComponent<AudioSource>().Play();
            if(isChasing || isEvading)
            {
                _Timer = 0;

                if (_Health > 1)
                {
                    if (other.gameObject.GetComponent<BulletBehaviour>()._BulletType == BulletType.ROCKET)
                    {
                        TakeDamage(0);
                    }

                    if ((other.gameObject.GetComponent<BulletBehaviour>()._BulletType == BulletType.SINGLE || other.gameObject.GetComponent<BulletBehaviour>()._BulletType == BulletType.SHOTGUN || other.gameObject.GetComponent<BulletBehaviour>()._BulletType == BulletType.SPIRAL))
                    {
                        if(this.gameObject != null)
                        {
                            TakeDamage(other.gameObject.GetComponent<BulletBehaviour>()._RegularDamage);
                        }
                        
                    }
                    

                    if (_HealthBarController != null)
                    {
                        if (other.gameObject.GetComponent<BulletBehaviour>()._BulletType == BulletType.SINGLE || other.gameObject.GetComponent<BulletBehaviour>()._BulletType == BulletType.SHOTGUN || other.gameObject.GetComponent<BulletBehaviour>()._BulletType == BulletType.SPIRAL)
                        {
                            _HealthBarController.TakeDamage(other.gameObject.GetComponent<BulletBehaviour>()._RegularDamage);
                        }


                    }
                    else
                    {
                        Debug.Log("null");
                    }
                }

                return;
            }

          
           
        }
    }
    void DeathSequence()
    {
        //play animation
        float rand = Random.Range(1, 360);
        this.gameObject.SetActive(false);
        HealthBar.gameObject.SetActive(false);
        switch (_EnemyType)
        {
            case EnemyType.BASIC:
                {
                    _BasicAnimator.gameObject.SetActive(false);
                    break;
                }
            case EnemyType.TANK:
                {
                    _TankAnimator.gameObject.SetActive(false);
                    break;
                }
            case EnemyType.EXPLOSIVE:
                {
                    _ExplodingAnimator.gameObject.SetActive(false);
                    _CorrosiveFloor.GetComponent<Transform>().position = this.gameObject.transform.position;
                    _CorrosiveFloor.SetActive(true);
                    //Explosive and spawn Corrosive Stain Blood;
                    break;
                }
        }
        _Blood_VFX_Prefab.GetComponent<Transform>().position = this.gameObject.transform.position;
        _StainPrefab.GetComponent<Transform>().position = this.gameObject.transform.position;
        _Blood_VFX_Prefab.SetActive(true);
        _Blood_VFX_Prefab.transform.Rotate(0, 0, rand);
        _StainPrefab.SetActive(true);
        _StainPrefab.transform.Rotate(0, 0, rand);
        
        SpawnManager.Instance.NotifyKill();
        UIManager.instance.IncreaseEnemiesKilled();
        
    }
    internal void ReturnEnemy()
    {
        _EnemyManager.ReturnEnemy(this.gameObject, _EnemyType);
    }
    protected Vector3 Wander()
    {
        float jitter = _Controller._Wander_Jitter * Time.deltaTime;//Getting jitter variable

        _Wander_Target += new Vector3(0, RandomBinomial() * jitter, 0);//Creating small random Vector
        _Wander_Target = _Wander_Target.normalized;
        _Wander_Target *= _Controller._Wander_Radius; // Increasing the lenght of the vector

        Vector3 _Target_In_LocalSpace = _Wander_Target + new Vector3(_Controller._Wander_Distance, _Controller._Wander_Distance, 0);
        Vector3 _Target_In_WorldSpace = transform.TransformPoint(_Target_In_LocalSpace);
        _Target_In_WorldSpace -= this._Position; //Stearing before we return it.

        return _Target_In_WorldSpace.normalized;
    }
    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        var neighbours = _LevelController.GetEnemyNeightbourhs(this, _Controller._Cohesion_Radius);

        if (neighbours.Count == 0)
        {
            return cohesionVector;
        }

        foreach (var member in neighbours)
        {
            if (isOnFOV(member._Position))
            {
                //Update cohesion vector
                cohesionVector += member._Position;
                countMembers++;
            }
        }
        if (countMembers == 0)
        {
            return cohesionVector;
        }
        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this._Position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }
    Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        var members = _LevelController.GetEnemyNeightbourhs(this, _Controller._Alignment_Radius);

        if (members.Count == 0)
        {
            return alignVector;
        }
        foreach (var member in members)
        {
            if (isOnFOV(member._Position))
            {
                alignVector += member._Velocity;
            }
        }

        return alignVector.normalized;

    }
    Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();
        var members = _LevelController.GetEnemyNeightbourhs(this, _Controller._Separation_Radius);
        if (members.Count == 0)
        {
            return separateVector;
        }
        foreach (var member in members)
        {
            if (isOnFOV(member._Position))
            {
                Vector3 MovingTowards = this._Position - member._Position;
                //Check the magnitude
                if (MovingTowards.magnitude > 0)
                {
                    separateVector += MovingTowards.normalized / MovingTowards.magnitude;
                }
            }
        }
        return separateVector.normalized;
    }
    public Vector3 Evade(Vector3 bulletTarget)
    {      //What is wrong with this calculation?

        //acceleration = Vector3.ClampMagnitude(acceleration, _Controller._Max_Acceleration);
        // velocity = velocity + acceleration * Time.deltaTime;
        //  velocity = Vector3.ClampMagnitude(velocity, _Controller._Max_Velocity);
        //   position = position + velocity * Time.deltaTime;

        Vector3 newVelocity = (_Position - bulletTarget).normalized * _Speed;
        isEvading = true;
        _Timer = 0;
        return rb.velocity = newVelocity - _Velocity;

    }
    virtual protected Vector3 Combine()
    {
        Vector3 FinalVector = _Controller._Cohesion_Priority * Cohesion() + _Controller._WanderPriority * Wander()
            + _Controller._Aligment_Priority * Alignment() + _Controller._Separation_Priority * Separation();
        return FinalVector;
    }
    float RandomBinomial()

    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }
    bool isOnFOV(Vector3 vec)
    {
        return Vector3.Angle(this._Velocity, -vec - this._Position) <= _Controller._Max_Field_Of_View;
    }

    //Vector3 Avoidance()
    //{
    //    Vector3 avoidanceVector = new Vector3();
    //    var bulletList = _LevelController.GetBullets(bulletReference.GetComponent<BulletBehaviour>(), _Controller._Avoidance_Radius);

    //    if (bulletList.Count == 0)
    //        return avoidanceVector;

    //    foreach (var bullet in bulletList)
    //    {
    //        avoidanceVector += Evade(bullet.transform.position);
    //    }

    //    return avoidanceVector.normalized;
    //}
    //Vector3 ChaseUpdated()
    //{
    //    Vector3 chasingVector = new Vector3();
    //    var player = _Target;
    //    if (player != null)//is not evading)
    //    {
    //        rb.velocity = new Vector2(Direction.x, Direction.y) * _Speed;
    //        _Velocity = rb.velocity;
    //        chasingVector = _Velocity;
    //    }
    //    return chasingVector;
    //}
    //void WrapAround(ref Vector3 vector, float min, float max)
    //{
    //    vector.x = WrapAroundFloat(vector.x, min, max);
    //    vector.y = WrapAroundFloat(vector.y, min, max);
    //    vector.z = WrapAroundFloat(vector.z, min, max);
    //}
    //public float WrapAroundFloat(float value, float min, float max)
    //{
    //    if (value > max)
    //        value = min;
    //    else if (value < min)
    //        value = max;
    //    return value;
    //}
}

