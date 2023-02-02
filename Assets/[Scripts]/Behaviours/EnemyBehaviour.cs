using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehaviour : MonoBehaviour
{
    public static EnemyBehaviour Instance;
    [SerializeField]private GameObject healthBarController;
    private EnemyHealthBarController controller;
    //public static event Action<EnemyBehaviour> OnDestroyedEnemy;
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
       controller = healthBarController.GetComponent<EnemyHealthBarController>();
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
            //EnemyCount.Instance.EnemyKilled();
            DeathSequence();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(50);
            if (controller != null)
            {
                controller.TakeDamage(50);
            }
            else
            {
                Debug.Log("null");
            }
           
            if (health <= 0)
            {
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    private void DeathSequence()
    {
        //play animation
        Destroy(this.gameObject, 0.5f);
        SpawnManager.Instance.NotifyKill();
        UIManager.instance.IncreaseEnemiesKilled();
        
    }
}

