using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundary horizontal;
    public Boundary vertical;
}



public class BulletBehaviour : MonoBehaviour
{
    static public BulletBehaviour instance;
    [Header("Bullet Properties")]
    public ScreenBounds bounds;
    public BulletType bulletType;
    private BulletManager bulletManager;
    public Vector3 targetPosition;
    public float speed = 5;
    private Rigidbody2D rb;
    public float damage = 30;

    public AI_Level_Manager _LevelController;
    void Awake()
    {
        instance = this;
        _LevelController = FindObjectOfType<AI_Level_Manager>();
    }
    private void OnEnable()
    {
       
        _LevelController.ListOfBullets.Add(this.gameObject.GetComponent<BulletBehaviour>());
        
    }
    private void OnDisable()
    {
        _LevelController.ListOfBullets.Remove(this.gameObject.GetComponent<BulletBehaviour>());
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletManager = FindObjectOfType<BulletManager>();
    }

    private void FixedUpdate()
    {
        CheckBounds();
    }
  
    void CheckBounds()
    {
        if ((transform.position.x > bounds.horizontal.max) ||
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min))
        {
            rb.velocity = Vector2.zero;
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            rb.velocity = Vector2.zero;
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
        
    }

}