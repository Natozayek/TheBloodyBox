using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject stainPrefab;
    public static EnemyBehaviour Instance;
    [SerializeField]private EnemyHealthBarController healthBarController;
    [SerializeField] public float health, maxtHealth = 100f;
    [SerializeField] public float speed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 Direction;
   

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        stainPrefab = Resources.Load<GameObject>("Prefabs/BloodStain");
        health = maxtHealth;
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
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
            speed = 0;
            DeathSequence();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (health <= 0)
            {
                this.gameObject.GetComponent<Collider2D>().isTrigger = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            TakeDamage(50);
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
        GameObject blood;
        blood =  Instantiate(stainPrefab, transform.position, Quaternion.identity);
        blood.transform.Rotate(0, 0, rand);
        Destroy(this.gameObject, 0.1f);
        SpawnManager.Instance.NotifyKill();
        UIManager.instance.IncreaseEnemiesKilled();
        
    }
}

