using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

//public class PlayerBehaviour2 : MonoBehaviour
//{
//    [Header("Movement Properties")]
//    public int horizontalForce;
//    public float verticalForce;
//    public float airFactor;
//    public int horizontalSpeed;
//    public Transform groundPoint;
//    public float groundRadius;
//    public LayerMask groundLayerMask, propsLayerMask;
//    public bool isGrounded, isGroundedInProps;
//    private Rigidbody2D rb;

//    [Header("Animations")]
//    public Animator animator;
 
   

//    [Header("Controls")]
//    public Joystick leftStick;
//    [Range(0.1f, 1.0f)]
//    public float verticalThreshold;

//    [Header("HealthSystem")] public HealthBarController health;
//    public int lifeCounter;
//    public int healthMax;

//    public DeathPlaneController DeathPlane;
//   // public LifeCounterController life;


//    private SoundManager soundManager;


//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        health = FindObjectOfType<PlayerHealth>().GetComponent<HealthBarController>();
//        soundManager = FindObjectOfType<SoundManager>();

//        leftStick = (Application.isMobilePlatform) ? GameObject.Find("LeftStick").GetComponent<Joystick>() : null;
//        DeathPlane = FindObjectOfType<DeathPlaneController>();
//        life = FindObjectOfType<LifeCounterController>();

//    }

//    // Update is called once per frame

//    void Update()
//    {

//        if (health.value <= 0)
//        {
//            life.LoseLife();

//            if (life.value > 0)
//            {
//                health.resetHeath();
//                DeathPlane.ReSpawn(gameObject);
//                soundManager.PlaySoundFX(SoundFX.DEATH,SoundChannel.PLAYER_DEATH_FX);
//            }
//        }

//        if (life.value <= 0)
//        {
//            SceneManager.LoadScene("EndScene");
//        }
//    }
//    void FixedUpdate()
//    {
//        var grounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
//        var groundedinProps = Physics2D.OverlapCircle(groundPoint.position, groundRadius, propsLayerMask);
//        isGrounded = grounded;
//        isGroundedInProps = groundedinProps;
//        if(isGrounded || isGroundedInProps)
//        {
//            animator.SetBool("isJumping", false);
//        }


//        Move();
//        Jump();

//    }

//    private void Jump()
//    {
//        var y = Input.GetAxis("Jump") + ((Application.isMobilePlatform) ? leftStick.Vertical : 0.0f);

//        if (isGrounded && (y >verticalThreshold) || isGroundedInProps && (y> verticalThreshold))
//        {
//            //do jumping

//            rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
//            soundManager.PlaySoundFX(SoundFX.JUMP,SoundChannel.PLAYER_JUMP_FX);
//            animator.SetBool("isJumping", true);
//        }
//    }

//    public void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;

//        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
//    }

//    private void Move()
//    {
        
//        var x = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? leftStick.Horizontal : 0.0f);
        
//        if(x != 0)
//        {
//            flip(x);
//            x = (x >0) ? 1 : -1;
            
//            rb.AddForce(Vector2.right * x * horizontalForce *((isGrounded || isGroundedInProps)? 1.0f : airFactor));


//            var clampXvel = Mathf.Clamp(rb.velocity.x, -horizontalSpeed, horizontalSpeed);
//            rb.velocity = new Vector2(clampXvel, rb.velocity.y);
            
//        }
//        animator.SetFloat("Speed", Mathf.Abs(x));

//    }

//    public void flip(float x)
//    {
//        if(x != 0.0f)
//        {
//            transform.localScale = new Vector3((x>0) ? 1.0f : -1.0f, 1.0f, 1.0f);
//        }
//    }

//    void OnCollisionEnter2D(Collision2D other)
//    {

//        if(other.gameObject.CompareTag("Enemy"))
//        {
//            health.TakeDamage(20);
//            if (life.value > 0)
//            {
//                soundManager.PlaySoundFX(SoundFX.HURT, SoundChannel.PLAYER_HURT_FX);

//                //TODO: Play the hurt sound
//            }
//        }
//    }
//}
