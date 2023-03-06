using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private SpawnManager spawnManager;
    public EnemyType enemType;
    public GameObject stainPrefab;
    public static EnemyBehaviour Instance;
    [SerializeField]private EnemyHealthBarController healthBarController;
    [SerializeField] public float health, maxtHealth = 100f;
    [SerializeField] public float speed = 5f;
    private EnemyManager enemyManager;
    Rigidbody2D rb;
    Transform target;
    Vector2 Direction;
   

    private void Awake()
    {
        Instance = this;
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
        if(spawnManager.intermissionOn)
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
    private void FixedUpdate()
    {
        if (target != null)
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
            if (health >= 1)
            {
                TakeDamage(50);

            }

            
            if (healthBarController != null)
            {
                healthBarController.TakeDamage(50);
            }
            else
            {
                Debug.Log("null");
            }
           
           
        }
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
}

