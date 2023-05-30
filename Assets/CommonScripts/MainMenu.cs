using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // From https://youtu.be/vviFh8HdsFw

    public GameObject settingsPanel;
    public GameObject tutorialPanel;
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(GamePreferencesManager.m_VolumeKey, 1f);
    }

    public void StartGame()
    {
        SaveData.PlayerData export = new SaveData.PlayerData
        {
            m_Health = 10,
            m_XP = 0,
            m_level = 1,
            m_rangedWeapon = WeaponReference.rangedWeapon.pistol,
            m_meleeWeapon = WeaponReference.meleeWeapon.dagger,
            m_lastScene = "MainMenu"
        };
        GamePreferencesManager.SavePrefs(export);
        SaveData.LevelData levelData = new SaveData.LevelData
        {
            island_0 = 1,
            island_1 = 0,
            island_2 = 0,
            island_3 = 0,
            island_4 = 0,
            island_5 = 0,
            island_6 = 0,
            island_7 = 0,
            island_8 = 0,
            level_0 = 0
        };
        GamePreferencesManager.SaveLevelData(levelData);
        SceneManager.LoadScene("IslandSelection");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayerPrefs.SetFloat(GamePreferencesManager.m_VolumeKey, volumeSlider.value);
        settingsPanel.SetActive(false);
    }

    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
