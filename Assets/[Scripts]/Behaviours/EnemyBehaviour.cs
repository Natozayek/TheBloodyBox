using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Properties")]
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float DetectionRadius;

    public AI_Level_Manager _LevelController;
    public AI_Controller _Controller;
    Vector3 _Wander_Target;
    
    
    public EnemyType enemType;
    public GameObject stainPrefab;
    public static EnemyBehaviour Instance;
    [SerializeField]private EnemyHealthBarController healthBarController;
    [SerializeField] public float health, maxtHealth = 100f;
    [SerializeField] public float speed = 5f;
    [SerializeField] public GameObject bulletReference;
    //Private properties
    private EnemyManager enemyManager;
    private SpawnManager spawnManager;
    private bool isChasing;

    Rigidbody2D rb;
    Transform target;
    Vector2 Direction;
    public bool isEvading;
    [SerializeField]private CircleCollider2D externalCollider;

    private void Awake()
    {
      
        Instance = this;
       
        isChasing = true;
        isEvading = false;
    }

    private void OnEnable()

    {
        _LevelController = FindObjectOfType<AI_Level_Manager>();
        _Controller = FindObjectOfType<AI_Controller>();
        _LevelController.ListOfEnemies.Add(this.gameObject.GetComponent<EnemyBehaviour>());
    }
    private void OnDisable()
    {
        _LevelController.ListOfEnemies.Remove(this.gameObject.GetComponent<EnemyBehaviour>());
    }
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        
        health = maxtHealth;
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
     
        if (spawnManager.intermissionOn)
        {
            DeathSequence();
        }
        if(target != null)
        {
            Vector3 tempDirection = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            Direction = tempDirection;
        }
    }

    public IEnumerator  StopEvasion()
    {
  
        yield return new WaitForSeconds(2);
        isEvading = false;
        isChasing = true;
    }

    private void FixedUpdate()
    {

        if (!isChasing)
        {
            AI_Movement();


        }
        if (isChasing)
        {
            Chase();
        }


        
    }

    private void AI_Movement()
    {
        position = transform.position;
        acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, _Controller._Max_Acceleration);
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, _Controller._Max_Velocity);
        position = position + velocity * Time.deltaTime;
        WrapAround(ref position, -_LevelController.bounds, _LevelController.bounds);
        transform.position = position;
    }

    private void Chase()
    {
        if (target != null)//is not evading)
        {
            rb.velocity = new Vector2(Direction.x, Direction.y) * speed;
        }
    
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            rb.velocity = Vector3.zero;
            health = 0;
         
            DeathSequence();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
            
        {
            if(isChasing)
            {
                isChasing = false;
                StartCoroutine(StartChasing());

                if (health >= 1)
                {
                    TakeDamage(other.gameObject.GetComponent<BulletBehaviour>().damage);

                }


                if (healthBarController != null)
                {
                    healthBarController.TakeDamage(other.gameObject.GetComponent<BulletBehaviour>().damage);
                }
                else
                {
                    Debug.Log("null");
                }
                return;
            }

            if (!isChasing)
            {
                if (health >= 1)
                {
                    TakeDamage(other.gameObject.GetComponent<BulletBehaviour>().damage);

                }


                if (healthBarController != null)
                {
                    healthBarController.TakeDamage(other.gameObject.GetComponent<BulletBehaviour>().damage);
                }
                else
                {
                    Debug.Log("null");
                }
            }
           
        }
    }

    private IEnumerator StartChasing()
    {
        yield return new WaitForSeconds(5);
        isChasing = true;
    }

    private void DeathSequence()
    {
        //play animation
        float rand = Random.Range(1, 360);
        this.gameObject.SetActive(false);
        
        stainPrefab.GetComponent<Transform>().position = this.gameObject.transform.position;
        stainPrefab.SetActive(true);
        stainPrefab.transform.Rotate(0, 0, rand);
        
        SpawnManager.Instance.NotifyKill();
        UIManager.instance.IncreaseEnemiesKilled();
        
    }

    internal void ReturnEnemy()
    {
        enemyManager.ReturnEnemy(this.gameObject, enemType);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, DetectionRadius-1);
    }


    protected Vector3 Wander()
    {
        float jitter = _Controller._Wander_Jitter * Time.deltaTime;//Getting jitter variable

        _Wander_Target += new Vector3(0, RandomBinomial() * jitter, 0);//Creating small random Vector
        _Wander_Target = _Wander_Target.normalized;
        _Wander_Target *= _Controller._Wander_Radius; // Increasing the lenght of the vector

        Vector3 _Target_In_LocalSpace = _Wander_Target + new Vector3(_Controller._Wander_Distance, _Controller._Wander_Distance, 0);
        Vector3 _Target_In_WorldSpace = transform.TransformPoint(_Target_In_LocalSpace);
        _Target_In_WorldSpace -= this.position; //Stearing before we return it.
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
            if (isOnFOV(member.position))
            {
                //Update cohesion vector
                cohesionVector += member.position;
                countMembers++;
            }
        }
        if (countMembers == 0)
        {
            return cohesionVector;
        }

        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
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
            if (isOnFOV(member.position))
            {
                alignVector += member.velocity;
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
            if (isOnFOV(member.position))
            {
                Vector3 MovingTowards = this.position - member.position;
                //Check the magnitude
                if (MovingTowards.magnitude > 0)
                {
                    separateVector += MovingTowards.normalized / MovingTowards.magnitude;
                }
            }
        }
        return separateVector.normalized;
    }

    Vector3 Avoidance()
    {
        Vector3 avoidanceVector = new Vector3();
        var bulletList = _LevelController.GetBullets(bulletReference.GetComponent<BulletBehaviour>(), _Controller._Avoidance_Radius);

        if (bulletList.Count == 0)
            return avoidanceVector;

        foreach (var bullet in bulletList)
        {
            avoidanceVector += Evade(bullet.transform.position);
        }

        return avoidanceVector.normalized;
    }

   public Vector3 Evade(Vector3 bulletTarget)
    {
        isEvading = true;
        Debug.Log(isEvading);
        acceleration = Vector3.ClampMagnitude(acceleration, _Controller._Max_Acceleration);
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, _Controller._Max_Velocity);
        position = position + velocity * Time.deltaTime;
        //WrapAround(ref position, -_LevelController.bounds, _LevelController.bounds);
        //transform.position = position;
        Vector3 newVelocity = (position - bulletTarget).normalized * _Controller._Max_Velocity;
        return rb.velocity = newVelocity - velocity;    
    }

    virtual protected Vector3 Combine()
    {
        Vector3 FinalVector = _Controller._Cohesion_Priority * Cohesion() + _Controller._WanderPriority * Wander()
            + _Controller._Aligment_Priority * Alignment() + _Controller._Separation_Priority * Separation() +
            _Controller._Avoidance_Priority * Avoidance();
        return FinalVector;
    }
    void WrapAround(ref Vector3 vector, float min, float max)
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
    public float RandomBinomial()

    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    bool isOnFOV(Vector3 vec)
    {

        return Vector3.Angle(this.velocity, -vec - this.position) <= _Controller._Max_Field_Of_View;
    }
}

