using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int numberOfProjectiles;           //Number of projectiles to shoot.  \\
    public float projectileSpeed;               // Speed of the projectile.                \\           
    public GameObject ProjectilePrefab;   // Prefab to spawn.                            \\
    public BulletPattern DesiredPattern;
    public float movementSpeed;
    public Transform firePoint;
    public Camera cam;
    public Rigidbody2D rb;

    //Testing SPIRAL PATTERN
    [Range(1f, 360f)]
    public float angleStepFixed = 0f;
    //Testing INCREMENTAL PATTERN
    [Range(1f, 45f)]
    public float IncrementalAngle = 0f;

    //Testing POINTING PATTERN
    public float projectileSpread;


    [Header("Private Variables")]
    private Vector3 startPoint;                 // Starting position of the bullet.         \\ 
    private const float radius = 1f;           //  Help us find the move direction.        \\
    private Vector2 mousePosition;         // Helps us to aim                                      \\
    private Vector2 movementDirection;


    // Update is called once per frame
    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            startPoint = transform.position;
            ShootProjectile(numberOfProjectiles);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
        Vector2 direction = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;// 
    }
    // Spawns x number of projectiles.
    private void ShootProjectile(int _numberOfProjectiles)
    {
        switch (DesiredPattern)
        {
            case BulletPattern.SPIRAL:
                float angleStep = 360 / _numberOfProjectiles;
                float angle = 0f;

                for (int i = 0; i <= _numberOfProjectiles - 1; i++)
                {
                    // Direction calculations.
                    float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                    float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                    // Create vectors.
                    Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
                    Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

                    // Create game objects.
                    GameObject SpiralBullet = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
                    SpiralBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);

                    // Destory the gameobject after 10 seconds.
                    Destroy(SpiralBullet, 2f);

                    angle += angleStep;
                }
                break;
            case BulletPattern.INCREMENTAL:

                float angleIncrease = IncrementalAngle;
                float angle2 = 0f;

                for (int i = 0; i <= numberOfProjectiles - 1; i++)
                {
                    // Direction calculations.
                    float projectileDirXPosition = startPoint.x + Mathf.Sin((angle2 * Mathf.PI) / 180) * radius;
                    float projectileDirYPosition = startPoint.y + Mathf.Cos((angle2 * Mathf.PI) / 180) * radius;

                    // Create vectors.
                    Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
                    Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

                    // Create game objects.
                    GameObject incrementalBullet = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
                    incrementalBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);

                    // Destory the gameobject after 5 seconds.
                    Destroy(incrementalBullet, 5f);

                    angle2 += angleIncrease;
                }
                break;
            case BulletPattern.SINGLE:
                float startRotation = firePoint.up.y + projectileSpread / 2f;

                //Instantiate bullet & adding force to projectile;
                GameObject Bullet = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D bulletRb = Bullet.GetComponent<Rigidbody2D>();
                bulletRb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);




                break;
            case BulletPattern.SHOTGUN:
                break;
            default:
                break;
        }
       
    }


}

public enum BulletPattern
{
    SPIRAL,
    INCREMENTAL,
    SINGLE,
    SHOTGUN
};
