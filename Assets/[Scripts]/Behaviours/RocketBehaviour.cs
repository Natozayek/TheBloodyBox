using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _ExplosionAnimation;
    [SerializeField] BulletBehaviour _BulletBehaviour;
    [SerializeField] Animator _Animator;
    public BulletType _BulletType;

    private void Start()
    {
        transform.position = _BulletBehaviour.transform.position;
        _Animator = GetComponentInChildren<Animator>();
        _BulletType = BulletType.ROCKET;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject != null)
        {
            Debug.Log("Collision");
            _Animator.enabled = true;

            _BulletBehaviour._RocketDamage = _BulletBehaviour._RegularDamage * 1.5F;
            _BulletBehaviour.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if(other.gameObject != null)
            {
                other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(_BulletBehaviour._RocketDamage);
                other.gameObject.GetComponent<EnemyBehaviour>()._HealthBarController.TakeDamage(_BulletBehaviour._RocketDamage);
       
        
            }
           
            StartCoroutine(_ExplosionSequence());

        }

    }

    
    IEnumerator _ExplosionSequence()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        _BulletBehaviour._Bullet_Manager.ReturnBullet(this._BulletBehaviour.gameObject, this._BulletBehaviour._BulletType);

    }

}
