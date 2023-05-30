using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Camera sceneCamera;
    public GameObject bullet;
    public Transform firepoint;
    public float fireForce = 5;
    private Vector3 mousePosition;
    public SpriteRenderer gun;
    private float aimAngle;
    private Quaternion aimRotation;
    public Transform target;
    private float damage;

    public delegate void Gunshot();
    public static event Gunshot OnFired;

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Look();
    }

    void ProcessInputs()
    {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Look()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        aimRotation = Quaternion.Euler(0f, 0f, aimAngle);

        if ((aimAngle > 0) || (aimAngle < 0 && aimAngle < -180))
        {
            gun.flipY = true;
        }
        else
        {
            gun.flipY = false;
        }
        transform.rotation = aimRotation;
    }

    public void fire()
    {
        OnFired?.Invoke();
        GameObject projectile = Instantiate(bullet, firepoint.position, firepoint.rotation);
        projectile.GetComponent<Bullet>().SetDamage(damage);
        projectile.GetComponent<Rigidbody2D>().AddForce(firepoint.right * fireForce, ForceMode2D.Impulse);
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}
