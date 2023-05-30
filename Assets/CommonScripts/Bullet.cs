using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float damage = 1;

    public delegate void BulletHit();
    public static event BulletHit OnHit;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            OnHit?.Invoke();
            enemyComponent.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            playerComponent.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bulletController))
        {
            //Do Nothing so we don't destroy scattergun bullets
        }
        else if (collision.gameObject.TryGetComponent<Interactable>(out Interactable pickupController))
        {
            //Do Nothing so we don't destroy when hitting pickup colliders
        }
        else
        {
            Destroy(gameObject);
        }
    }
}