using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Sensing Parameters")]
    public LayerMask collisionLayerMask;
    public bool isPlayerInRange;
    public bool canSeeThePlayer;
    public Collider2D colliderName;


    private Transform playerTransform;
    private float playerDirection;
    private float enemyDirection;

    private Vector2 playerDirectionVector;

    void Start()
    {
        playerDirectionVector = Vector2.zero;
        playerDirection = 0;
       
        isPlayerInRange = false;
        canSeeThePlayer = false;
        playerTransform = FindObjectOfType<PlayerBehaviour>().transform;
    }

    
    void Update()
    {
        if(isPlayerInRange)
        {
            var lineofSight = Physics2D.Linecast(transform.position, playerTransform.position, collisionLayerMask);
            colliderName = lineofSight.collider;
           // enemyDirection = enemyDirection = GetComponentInParent<EnemyBehaviour>().direction.x;
            playerDirectionVector = (playerTransform.position - transform.position);
            playerDirection = (playerDirectionVector.x > 0) ? 1.0f : -1.0f;

            

            canSeeThePlayer = (lineofSight.collider.gameObject.name == "Player") && (playerDirection == enemyDirection); // if both are correct enemy has line of sight - can see the player
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            isPlayerInRange = true; 
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = (canSeeThePlayer) ? Color.green : Color.red;
        if(isPlayerInRange)
        {
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
        Gizmos.color = (isPlayerInRange) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }
}
