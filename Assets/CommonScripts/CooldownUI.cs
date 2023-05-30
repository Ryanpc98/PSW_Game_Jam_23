using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image sr;
    [SerializeField] Sprite pistol;
    [SerializeField] Sprite blunderbuss;
    [SerializeField] Sprite scattergun;
    [SerializeField] Sprite doublePistols;
    [SerializeField] Sprite dagger;
    [SerializeField] Sprite rapier;
    [SerializeField] Sprite cutlass;
    [SerializeField] Sprite axe;

    private float timeFired;
    private float fireRate;
    private bool needToAnimate = false;

    private void Awake()
    {
        slider.value = 1;
    }

    public void animateCooldownBar(float tempFireRate)
    {
        timeFired = Time.time;
        fireRate = tempFireRate;
        needToAnimate = true;
    }

    public void changeEquippedWeapon(WeaponReference.rangedWeapon weaponToEquip)
    {
        Debug.Log($"Changing Ranged UI to {weaponToEquip}");
        switch (weaponToEquip)
        {

            case WeaponReference.rangedWeapon.pistol:
                sr.sprite = pistol;
                break;
            case WeaponReference.rangedWeapon.scattergun:
                sr.sprite = scattergun;
                break;
            case WeaponReference.rangedWeapon.blunderbuss:
                sr.sprite = blunderbuss;
                break;
            case WeaponReference.rangedWeapon.doublePistols:
                sr.sprite = doublePistols;
                break;
            default:
                break;
        }
    }
    public void changeEquippedWeapon(WeaponReference.meleeWeapon weaponToEquip)
    {
        Debug.Log($"Changing Melee UI to {weaponToEquip}");
        switch (weaponToEquip)
        {
            case WeaponReference.meleeWeapon.dagger:
                sr.sprite = dagger;
                break;
            case WeaponReference.meleeWeapon.rapier:
                sr.sprite = rapier;
                break;
            case WeaponReference.meleeWeapon.cutlass:
                sr.sprite = cutlass;
                break;
            case WeaponReference.meleeWeapon.axe:
                sr.sprite = axe;
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if (needToAnimate)
        {
            var diff = Time.time - timeFired;
            if (diff < 1 / fireRate)
            {
                slider.value = diff / (1 / fireRate);
            }
            else
            {
                slider.value = 1;
                needToAnimate = false;
            }
        }
    }
}
