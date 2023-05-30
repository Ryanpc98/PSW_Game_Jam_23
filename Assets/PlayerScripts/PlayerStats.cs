using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public PlayerController player;

    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerKilled;

    public delegate void PlayerLevel();
    public static event PlayerLevel OnPlayerLevelUp;

    private float health;
    private float maxHealth;
    private float playerXP;
    private int currentLevel;
    private static float XPToLevel = 50f;

    [SerializeField] UIHealthBar healthBar;
    [SerializeField] UIXPBar XPBar;
    [SerializeField] GameManager gameManager;

    SaveData.PlayerData save;

  
    private void Awake()
    {
    }

    private void Start()
    {
        Debug.Log("Loading Prefs");
        save = GamePreferencesManager.LoadPrefs();
        health = save.m_Health;
        playerXP = save.m_XP;
        currentLevel = save.m_level;
        maxHealth = currentLevel * 10;
        Debug.Log($"hp: {health}, xp: {playerXP}, level: {currentLevel}, maxhp: {maxHealth}");
        healthBar.UpdateHealthBar(health, maxHealth);
        XPBar.UpdateLevel(currentLevel);
        XPBar.UpdateXPBar(playerXP, XPToLevel);
    }

    public SaveData.PlayerData GetPlayerSaveData()
    {
        SaveData.PlayerData export = new SaveData.PlayerData
        {
            m_Health = health,
            m_XP = playerXP,
            m_level = currentLevel,
            m_rangedWeapon = player.GetEquippedRangedWeapon(),
            m_meleeWeapon = player.GetEquippedMeleeWeapon()
        };
        return export;
    }

    public void ApplyDamage(float damageAmount)
    {
        Debug.Log($"Damage Amount: {damageAmount}");
        health -= damageAmount;
        Debug.Log($"Health is now {health}");
        healthBar.UpdateHealthBar(health, maxHealth);
        gameManager.PlayerDamageSfx();

        if (health <= 0)
        {
            Destroy(player);
            OnPlayerKilled?.Invoke();
        }
    }

    public bool ApplyHealing(float healAmount)
    {
        if (health >= maxHealth)
        {
            Debug.Log("Health at max, returning");
            return false;
        }
        else
        {
            Debug.Log($"Heal Amount: {healAmount}");
            if (healAmount < 0)
            {
                health += maxHealth * -healAmount;
            }
            else if (health + healAmount > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += healAmount;
            }
            Debug.Log($"Health is now {health}");
            healthBar.UpdateHealthBar(health, maxHealth);
            return true;
        }
    }

    public float getCurrentHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void awardXP(float XP)
    {
        Debug.Log($"XP Amount: {XP}");
        playerXP += XP;
        Debug.Log($"XP is now {playerXP}");

        if (playerXP >= XPToLevel)
        {
            OnPlayerLevelUp?.Invoke();
            currentLevel += 1;
            playerXP -= XPToLevel;
            Debug.Log($"Level Up, XP is now {playerXP}");
            XPBar.UpdateLevel(currentLevel);
            maxHealth += 5;
            health = maxHealth;
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        XPBar.UpdateXPBar(playerXP, XPToLevel);
    }
}
