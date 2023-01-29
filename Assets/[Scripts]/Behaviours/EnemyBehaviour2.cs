using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehaviour2 : MonoBehaviour
{
    public static event Action<EnemyBehaviour2> OnDestroyedEnemy;
    [SerializeField] float health, maxtHealth = 100f;
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 Direction;

    void Start()
    {
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
        if(health <= 0)
        {
            rb.velocity = Vector3.zero;
            speed = 0;
            DeathSequence();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            TakeDamage(50);
        }
    }
    private void DeathSequence()
    {
        //play animation
        Destroy(gameObject, 1);
    }
}
