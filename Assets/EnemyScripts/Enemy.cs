using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float health, maxHealth = 3f;
    private float XP = 5;
    private int currentLevel = 1;

    private Slider slider;
    private float moveSpeed = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    public float spacing = 1f;

    private Animator animator;

    private RaycastHit hit;
    private Vector3 rayDirection;

    public EnemyWeapon weapon;
    public CutlassAttack cutlass;

    public float fireRate = 1f;
    private float lastFired;
    public float attackRange = 0.3f;

    private float timeSpotted;
    private float fireDelay;
    private bool waitingToFire;
    private bool onScreen = false;
    private bool activated = false;

    [SerializeField] FloatingHealthBar healthBar;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        target = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void LevelUp(int level)
    {
        Debug.Log($"Setting Scene to {level}");
        if(gameObject.tag == "MeleeEnemy")
        {
            currentLevel = level;
            maxHealth = 5 * level;
            health = maxHealth;
            cutlass.SetDamage(5 + (2.5f * (level - 1)));
            XP = 5;
            moveSpeed = (0.25f + (0.05f * (level - 1)));
            attackRange = 0.3f;
            spacing = 0.2f;
            fireDelay = 0.2f;
        }
        else if (gameObject.tag == "RangedEnemy")
        {
            currentLevel = level;
            maxHealth = 3 * level;
            health = maxHealth;
            weapon.SetDamage(2 + (0.5f * (level - 1)));
            XP = 5;
            moveSpeed = (0.4f + (0.05f * (level - 1)));
            attackRange = 0.2f;
            spacing = 1f;
            fireDelay = 0.6f;
        }
        else if (gameObject.tag == "RangedMiniBoss")
        {
            currentLevel = level;
            maxHealth = 6 * level;
            health = maxHealth;
            weapon.SetDamage(4 + (0.5f * (level - 1)));
            XP = 20;
            moveSpeed = (0.6f + (0.05f * (level - 1)));
            attackRange = 0.2f;
            spacing = 1f;
            fireDelay = 0.3f;
        }
        else if (gameObject.tag == "MeleeMiniBoss")
        {
            currentLevel = level;
            maxHealth = 10 * level;
            health = maxHealth;
            cutlass.SetDamage(7 + (3f * (level - 1)));
            XP = 20;
            moveSpeed = (0.35f + (0.05f * (level - 1)));
            attackRange = 0.4f;
            spacing = 0.3f;
            fireDelay = 0.3f;
        }
        else if (gameObject.tag == "FinalBoss")
        {
            Debug.Log($"Setting Up Final Boss to {level}");
            currentLevel = level;
            maxHealth = 10 * level;
            health = maxHealth;
            cutlass.SetDamage(10 + (3f * (level - 1)));
            weapon.SetDamage(6 + (1f * (level - 1)));
            XP = 30f;
            moveSpeed = (0.2f + (0.05f * (level - 1)));
            attackRange = 0.5f;
            spacing = 0.7f;
            fireDelay = 0.3f;
        }
    }

    private void OnBecameVisible()
    {
        onScreen = true;
        activated = true;
    }

    private void OnBecameInvisible()
    {
        onScreen = false;
    }

    private void Update()
    {
        if (target)
        {
            moveDirection = (target.position - transform.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        if (target && activated)
        {
            var distanceToTarget = Vector2.Distance(target.position, transform.position);
            if (distanceToTarget >= spacing)
            {
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
            }
            else if (distanceToTarget <= (0.5 * spacing))
            {
                rb.velocity = new Vector2(-moveDirection.x, -moveDirection.y) * moveSpeed;
            }
            else
            {
                rb.velocity = new Vector2(0f, 0f);
            }
            if (onScreen)
            {
                SetAnimatorMovement(moveDirection);
                var los = CheckLineOfSight();
                if (los && distanceToTarget <= attackRange)
                {
                    if (gameObject.tag == "MeleeEnemy" || gameObject.tag == "MeleeMiniBoss" || gameObject.tag == "FinalBoss")
                    {
                        Debug.Log("Swinging");
                        cutlass.Swing(false);
                    }
                }
                else if (los && distanceToTarget >= attackRange)
                {
                    if (gameObject.tag == "RangedEnemy" || gameObject.tag == "RangedMiniBoss" || gameObject.tag == "FinalBoss")
                    {
                        weapon.fire();
                    }
                }
            }
        }
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }


    private bool CheckLineOfSight()
    {
        var rayDirection = target.position - transform.position;
        var currentTime = Time.time;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, rayDirection * hit.distance, Color.yellow);
            //Debug.Log($"{hit.collider.name} Hit");
            if (hit.collider.name == "Player")
            {
                Debug.DrawRay(transform.position, rayDirection * hit.distance, Color.red);
                //Debug.Log($"{hit.collider.name} Hit");
                if (waitingToFire == false)
                {
                    timeSpotted = currentTime;
                    waitingToFire = true;
                    return false;
                }
                else if ((currentTime - lastFired > 1 / fireRate) && (currentTime - timeSpotted > fireDelay) && waitingToFire)
                {
                    Debug.Log("Firing");
                    lastFired = currentTime;
                    return true;
                }
            }
            else if(waitingToFire == true && currentTime - timeSpotted > fireDelay)
            {
                waitingToFire = false;
                return false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, rayDirection * 1000, Color.white);
            //Debug.Log("Did not Hit");
            if (waitingToFire == true && currentTime - timeSpotted > fireDelay)
            {
                waitingToFire = false;
                return false;
            }
        }
        return false;
    }

    public void TakeDamage(float damageAmount)
    {
        //Already dead, multiple shots triggered multiple calls to this func
        if (health <= 0)
        {
            return;
        }
        if(gameObject.tag == "MeleeEnemy")
        {
            gameManager.ZombieDamageSfx();
        }
        else
        {
            gameManager.SkeletonDamageSfx();
        }
        Debug.Log($"Damage Amount: {damageAmount}");
        health -= damageAmount;
        Debug.Log($"Health is now {health}");
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            playerStats.awardXP(XP);
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
            gameManager.TrackDeaths(this);
        }
    }
}
