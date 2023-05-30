using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public Weapon weapon;
    public CutlassAttack cutlass;
    public DaggerAttack dagger;
    public RapierAttack rapier;
    public AxeAttack axe;

    private Animator animator;

    [SerializeField] private WeaponReference.rangedWeapon equippedRangedWeapon;
    [SerializeField] private WeaponReference.meleeWeapon equippedMeleeWeapon;

    public GameObject InteractIcon;
    private Vector2 boxSize = new Vector2(0.7f, 0.7f);

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private CooldownUI meleeCooldownUI;
    [SerializeField] private CooldownUI rangedCooldownUI;

    SaveData.PlayerData save;

    private void Awake()
    {
        save = GamePreferencesManager.LoadPrefs();
        PlayerEquipRangedWeapon(save.m_rangedWeapon);
        PlayerEquipMeleeWeapon(save.m_meleeWeapon);
        weapon.equipWeapon(equippedRangedWeapon);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!PauseMenu.gameIsPaused)
        {
            ProcessInputs();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0))
        {
            CallRangedAttack();
        }
        if (Input.GetMouseButton(1))
        {
            CallMeleeAttack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacting");
            CheckInteraction();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        SetAnimatorMovement(moveDirection);
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }

    public void PlayerEquipRangedWeapon(WeaponReference.rangedWeapon weaponToEquip)
    {
        equippedRangedWeapon = weaponToEquip;
        weapon.equipWeapon(weaponToEquip);
        rangedCooldownUI.changeEquippedWeapon(weaponToEquip);
    }

    public WeaponReference.rangedWeapon GetEquippedRangedWeapon()
    {
        return equippedRangedWeapon;
    }

    public void PlayerEquipMeleeWeapon(WeaponReference.meleeWeapon weaponToEquip)
    {
        equippedMeleeWeapon = weaponToEquip;
        meleeCooldownUI.changeEquippedWeapon(weaponToEquip);
    }

    public WeaponReference.meleeWeapon GetEquippedMeleeWeapon()
    {
        return equippedMeleeWeapon;
    }

    private void CallRangedAttack()
    {
        switch (equippedRangedWeapon)
        {
            case WeaponReference.rangedWeapon.pistol:
                Debug.Log("Firing Pistol");
                weapon.firePistol();
                break;
            case WeaponReference.rangedWeapon.scattergun:
                Debug.Log("Firing Scattergun");
                weapon.fireScattergun();
                break;
            case WeaponReference.rangedWeapon.blunderbuss:
                Debug.Log("Firing Blunderbuss");
                weapon.fireBlunderbuss();
                break;
            case WeaponReference.rangedWeapon.doublePistols:
                Debug.Log("Firing Double Pistols");
                weapon.fireDoublePistols();
                break;
            default:
                break;
        }
    }

    private void CallMeleeAttack()
    {
        switch (equippedMeleeWeapon)
        {
            case WeaponReference.meleeWeapon.dagger:
                Debug.Log("Swinging Dagger");
                dagger.Swing(true);
                break;
            case WeaponReference.meleeWeapon.rapier:
                Debug.Log("Swinging Rapier");
                rapier.Swing(true);
                break;
            case WeaponReference.meleeWeapon.axe:
                Debug.Log("Swinging Axe");
                axe.Swing(true);
                break;
            case WeaponReference.meleeWeapon.cutlass:
                Debug.Log("Swinging Cutlass");
                cutlass.Swing(true);
                break;
            default:
                break;
        }
    }

    public void OpenInteractableIcon(Transform interactTarget)
    {
        InteractIcon.transform.position = interactTarget.position - new Vector3(0f, 0.05f, 0f);
        InteractIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        InteractIcon.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        playerStats.ApplyDamage(damage);
    }

    public bool HealDamage(float health)
    {
        return playerStats.ApplyHealing(health);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position - new Vector3(1f, 0.15f, 0f), boxSize, 0f, Vector2.right, 0.7f);
        if(hits.Length > 0)
        {
            Debug.Log("In Raycast Hit");
            Debug.Log(hits);
            foreach (RaycastHit2D rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    Debug.Log("In Raycast Interactable Hit");
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}
