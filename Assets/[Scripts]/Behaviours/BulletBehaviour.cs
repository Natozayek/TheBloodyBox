using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour
{
    static public BulletBehaviour _Instance;
    [Header("Bullet Properties")]
    public float _RegularDamage = 0;
    public float _RocketDamage = 0;
    public float _Speed = 0;
    public BulletType _BulletType;
    [SerializeField] GameObject _Child_RocketReference;
    public BulletManager _Bullet_Manager;

    //Private Variables
    private AI_Level_Manager _Level_Controller;
    public Rigidbody2D RigidBody2D;
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
        RigidBody2D = GetComponent<Rigidbody2D>();
        _Bullet_Manager = FindObjectOfType<BulletManager>();
    }

     void Update()
    {
        _Timer = _Timer + Time.deltaTime;

        if(_Timer >= 4.0f)
        {
            RigidBody2D.velocity = Vector2.zero;
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
          
                _RocketDamage = 0;
                _Child_RocketReference.gameObject.SetActive(true);
                GetComponent<SpriteRenderer>().sprite = null;
                
                //Create Bullet parent, separate explosion from BulletPrefab - DONE
                //Set active the Explosion Game Object. -DONE
                //Call ExplosionSequence -DONE
                //After the animation set of the gameobject -DONE


            }
            else
            {
                RigidBody2D.velocity = Vector2.zero;
                _Bullet_Manager.ReturnBullet(this.gameObject, _BulletType);
            }
           
        }
        
    }

}