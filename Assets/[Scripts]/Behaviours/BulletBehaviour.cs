using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour
{
    static public BulletBehaviour _Instance;
    [Header("Bullet Properties")]
    public float _Speed = 5;
    public float _RegularDamage = 30;
    public float _RocketDamage = 30;
    public BulletType _BulletType;
    [SerializeField] GameObject _RocketReference;
    
    private AI_Level_Manager _Level_Controller;
    private BulletManager _Bullet_Manager;
    private Rigidbody2D rb;
    float _Timer;
    
    void Awake()
    {
        _Instance = this;
        _Level_Controller = FindObjectOfType<AI_Level_Manager>();
    }
    private void OnEnable()
    {
       
        _Level_Controller.ListOfBullets.Add(this.gameObject.GetComponent<BulletBehaviour>());
        _Timer = 0;
        
    }
    private void OnDisable()
    {
        _Level_Controller.ListOfBullets.Remove(this.gameObject.GetComponent<BulletBehaviour>());
        _Timer = 0;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _Bullet_Manager = FindObjectOfType<BulletManager>();
    }

     void Update()
    {
        _Timer = _Timer + Time.deltaTime;

        if(_Timer >= 10.0f)
        {
            rb.velocity = Vector2.zero;
            _Bullet_Manager.ReturnBullet(this.gameObject, _BulletType);
            _Timer = 0;
        }

        //CheckBounds();
    }
  
    //void CheckBounds()
    //{
    //    if ((transform.position.x > bounds.horizontal.max) ||
    //        (transform.position.x < bounds.horizontal.min) ||
    //        (transform.position.y > bounds.vertical.max) ||
    //        (transform.position.y < bounds.vertical.min))
    //    {
    //        rb.velocity = Vector2.zero;
    //        bulletManager.ReturnBullet(this.gameObject, bulletType);
    //    }
    //}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_BulletType == BulletType.ROCKET)
            {
                rb.velocity = Vector2.zero;
                _RocketReference.gameObject.SetActive(true);
                //Create Bullet parent, separate explosion from BulletPrefab
                //Set active the Explosion Game Object.
                //Call ExplosionSequence
                //After the animation set of the gameobject


            }
            else
            {
                rb.velocity = Vector2.zero;
                _Bullet_Manager.ReturnBullet(this.gameObject, _BulletType);
            }
           
        }
        
    }

}