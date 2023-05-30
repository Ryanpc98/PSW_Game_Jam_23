using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera sceneCamera;

    public GameObject bullet;
    public GameObject blunderbussRound;

    public Transform pistolFirepoint;
    public Transform blunderbussFirepoint;
    public Transform scattergunFirepoint;
    public Transform doublePistolFirepointRight;
    public Transform doublePistolFirepointLeft;


    public float fireForce = 5;

    private Vector3 mousePosition;

    public SpriteRenderer pistol;
    public SpriteRenderer scattergun;
    public SpriteRenderer blunderbuss;
    public SpriteRenderer doublePistols;

    public Transform pistolObj;
    public Transform scattergunObj;
    public Transform blunderbussObj;
    public Transform doublePistolsObj;


    private SpriteRenderer equippedGunSprite;
    private WeaponReference.rangedWeapon equippedGun;

    private float aimAngle;
    private Quaternion aimRotation;

    public float fireRate = 1;
    public float lastFired;

    private bool fireRightGun;
    private int shotCount;
    private float shotSpread;

    private float pistolDmg = 4f;
    private float blunderbussDmg = 8f;
    private float scattergunDmg = 2f;

    public delegate void Gunshot();
    public static event Gunshot OnFired;

    [SerializeField] private CooldownUI cooldownUI;

    private void Awake()
    {
        equippedGunSprite = scattergun;
        equippedGun = WeaponReference.rangedWeapon.scattergun;
        shotCount = 5;
        shotSpread = 30f;
    }

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

    public void equipWeapon(WeaponReference.rangedWeapon weaponToEquip)
    {
        Debug.Log($"Entering Equipping Func with {weaponToEquip}");
        switch (equippedGun)
        {
            case WeaponReference.rangedWeapon.pistol:
                Debug.Log("Unequipping Pistol");
                pistolObj.gameObject.SetActive(false);
                break;
            case WeaponReference.rangedWeapon.scattergun:
                Debug.Log("Unequipping Scattergun");
                scattergunObj.gameObject.SetActive(false);
                break;
            case WeaponReference.rangedWeapon.blunderbuss:
                Debug.Log("Unequipping Blunderbuss");
                blunderbussObj.gameObject.SetActive(false);
                break;
            case WeaponReference.rangedWeapon.doublePistols:
                Debug.Log("Unequipping Double Pistols");
                doublePistolsObj.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        switch (weaponToEquip)
        {
            case WeaponReference.rangedWeapon.pistol:
                Debug.Log("Equipping Pistol");
                pistolObj.gameObject.SetActive(true);
                equippedGunSprite = pistol;
                fireRate = 1f;
                break;
            case WeaponReference.rangedWeapon.scattergun:
                Debug.Log("Equipping Scattergun");
                scattergunObj.gameObject.SetActive(true);
                equippedGunSprite = scattergun;
                fireRate = 0.5f;
                break;
            case WeaponReference.rangedWeapon.blunderbuss:
                Debug.Log("Equipping Blunderbuss");
                blunderbussObj.gameObject.SetActive(true);
                equippedGunSprite = blunderbuss;
                fireRate = 0.25f;
                break;
            case WeaponReference.rangedWeapon.doublePistols:
                Debug.Log("Equipping Double Pistols");
                doublePistolsObj.gameObject.SetActive(true);
                equippedGunSprite = doublePistols;
                fireRate = 2f;
                break;
            default:
                break;
        }
        equippedGun = weaponToEquip;
    }

    void Look()
    {
        Vector2 aimDirection = mousePosition - transform.position;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        aimRotation = Quaternion.Euler(0f, 0f, aimAngle);

        if ((aimAngle > 0) || (aimAngle < 0 && aimAngle < -180))
        {
            equippedGunSprite.flipY = true;
        }
        else
        {
            equippedGunSprite.flipY = false;
        }
        transform.rotation = aimRotation;
    }

    public void firePistol()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            OnFired?.Invoke();
            lastFired = Time.time;
            GameObject projectile = Instantiate(bullet, pistolFirepoint.position, pistolFirepoint.rotation);
            projectile.GetComponent<Bullet>().SetDamage(pistolDmg);
            projectile.GetComponent<Rigidbody2D>().AddForce(pistolFirepoint.right * fireForce, ForceMode2D.Impulse);
            cooldownUI.animateCooldownBar(fireRate);
        }
    }
    public void fireBlunderbuss()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            OnFired?.Invoke();
            lastFired = Time.time;
            GameObject projectile = Instantiate(blunderbussRound, blunderbussFirepoint.position, blunderbussFirepoint.rotation);
            projectile.GetComponent<Bullet>().SetDamage(blunderbussDmg);
            projectile.GetComponent<Rigidbody2D>().AddForce(blunderbussFirepoint.right * fireForce, ForceMode2D.Impulse);
            cooldownUI.animateCooldownBar(fireRate);
        }
    }
    public void fireScattergun()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            OnFired?.Invoke();
            lastFired = Time.time;
            for (int i = 0; i < shotCount; i++)
            {
                var spread = Random.Range(-shotSpread / 2, shotSpread / 2);
                GameObject projectile = Instantiate(bullet, scattergunFirepoint.position, scattergunFirepoint.rotation);
                projectile.GetComponent<Bullet>().SetDamage(scattergunDmg);
                projectile.GetComponent<Rigidbody2D>().AddForce((Quaternion.AngleAxis(spread, Vector3.forward) * scattergunFirepoint.right) * fireForce, ForceMode2D.Impulse);
                cooldownUI.animateCooldownBar(fireRate);
            }
        }
    }
    public void fireDoublePistols()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            OnFired?.Invoke();
            lastFired = Time.time;
            if (fireRightGun)
            {
                GameObject projectile = Instantiate(bullet, doublePistolFirepointRight.position, doublePistolFirepointRight.rotation);
                projectile.GetComponent<Bullet>().SetDamage(pistolDmg);
                projectile.GetComponent<Rigidbody2D>().AddForce(doublePistolFirepointRight.right * fireForce, ForceMode2D.Impulse);
                cooldownUI.animateCooldownBar(fireRate);
                fireRightGun = false;
            }
            else
            {
                GameObject projectile = Instantiate(bullet, doublePistolFirepointLeft.position, doublePistolFirepointLeft.rotation);
                projectile.GetComponent<Bullet>().SetDamage(pistolDmg);
                projectile.GetComponent<Rigidbody2D>().AddForce(doublePistolFirepointLeft.right * fireForce, ForceMode2D.Impulse);
                cooldownUI.animateCooldownBar(fireRate);
                fireRightGun = true;
            }
        }
    }
    public void SetPistolDmg(float dmg)
    {
        pistolDmg = dmg;
    }
    public void SetBlunderbussDmg(float dmg)
    {
        blunderbussDmg = dmg;
    }
    public void SetScattergunDmg(float dmg)
    {
        scattergunDmg = dmg;
    }
}
