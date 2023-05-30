using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void SceneStatus();
    public static event SceneStatus OnLevelCleared;
    public static event SceneStatus OnBossCleared;

    IDictionary<string, int> sceneLevels = new Dictionary<string, int>()
    {
        {"Level_0", 1},
        {"Island_0", 1},
        {"Island_1", 2},
        {"Island_2", 2},
        {"Island_3", 3},
        {"Island_4", 6},
        {"Island_5", 4},
        {"Island_6", 4},
        {"Island_7", 5},
        {"Island_8", 5}
    };
    private int currentSceneLevel = 1;

    List<Enemy> listOfOpponents = new List<Enemy>();
    PlayerStats playerStats;

    public AudioSource playerDmg;
    public AudioSource zombieDmg;
    public AudioSource skeletonDmg;
    public AudioSource gunshot;
    public AudioSource hitMarker;
    public AudioSource clang;

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        AudioListener.volume = PlayerPrefs.GetFloat(GamePreferencesManager.m_VolumeKey, 0.75f);

        var scene = SceneManager.GetActiveScene().name;
        if (sceneLevels.ContainsKey(scene))
        {
            currentSceneLevel = sceneLevels[scene];
            Debug.Log($"Scene Found, Setting Level to {currentSceneLevel}");
            PlayerPrefs.SetString(GamePreferencesManager.m_LastSceneKey, name);
        }
        else
        {
            currentSceneLevel = 1;
            Debug.Log($"Scene not found, defaulting to 1");
        }

        listOfOpponents.AddRange(GameObject.FindObjectsOfType<Enemy>());
        Debug.Log($"Initial number of opps - {listOfOpponents.Count}");

        LevelUpScene();
    }

    private void OnEnable()
    {
        //PlayerStats.OnPlayerKilled += LoadMenu;
        Weapon.OnFired += GunshotSfx;
        EnemyWeapon.OnFired += GunshotSfx;
        Bullet.OnHit += HitMarkerSfx;
        PlayerStats.OnPlayerLevelUp += playClang;
    }

    private void OnDisable()
    {
        //PlayerStats.OnPlayerKilled -= LoadMenu;
        Weapon.OnFired -= GunshotSfx;
        EnemyWeapon.OnFired -= GunshotSfx;
        Bullet.OnHit -= HitMarkerSfx;
        PlayerStats.OnPlayerLevelUp -= playClang;
    }

    private void LevelUpScene()
    {
        foreach (var opp in listOfOpponents)
        {
            opp.LevelUp(currentSceneLevel);
        }
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TrackDeaths(Enemy opp)
    {
        if (listOfOpponents.Contains(opp))
        {
            listOfOpponents.Remove(opp);
        }
        Debug.Log($"Number of opps - {listOfOpponents.Count}");
        if(listOfOpponents.Count <= 0)
        {
            if (SceneManager.GetActiveScene().name == "Island_4")
            {
                SavePrefs();
                OnBossCleared?.Invoke();
            }
            else
            {
                SavePrefs();
                OnLevelCleared?.Invoke();
            }
        }
    }
    private void SaveAndReturnToMenu()
    {
        SaveData.PlayerData save = playerStats.GetPlayerSaveData();
        save.m_lastScene = SceneManager.GetActiveScene().name;
        GamePreferencesManager.SavePrefs(save);
        SceneManager.LoadScene("IslandSelection");
    }

    public void SavePrefs()
    {
        SaveData.PlayerData save = playerStats.GetPlayerSaveData();
        save.m_lastScene = SceneManager.GetActiveScene().name;
        GamePreferencesManager.SavePrefs(save);
        //SceneManager.LoadScene("IslandSelection");
    }

    public void PlayerDamageSfx()
    {
        playerDmg.Play();
    }
    public void ZombieDamageSfx()
    {
        zombieDmg.Play();
    }
    public void SkeletonDamageSfx()
    {
        skeletonDmg.Play();
    }
    public void GunshotSfx()
    {
        gunshot.Play();
    }
    public void HitMarkerSfx()
    {
        hitMarker.Play();
    }

    public void playClang()
    {
        clang.Play();
    }
}
