using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IslandSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject playerIcon;
    [SerializeField] GameObject island_1_button;
    [SerializeField] GameObject island_2_button;
    [SerializeField] GameObject island_3_button;
    [SerializeField] GameObject island_4_button;
    [SerializeField] GameObject island_5_button;
    [SerializeField] GameObject island_6_button;
    [SerializeField] GameObject island_7_button;
    [SerializeField] GameObject island_8_button;
    [SerializeField] GameObject island_0_button;

    [SerializeField] Sprite island_1_c;
    [SerializeField] Sprite island_2_c;
    [SerializeField] Sprite island_3_c;
    [SerializeField] Sprite island_4_c;
    [SerializeField] Sprite island_5_c;
    [SerializeField] Sprite island_6_c;
    [SerializeField] Sprite island_7_c;
    [SerializeField] Sprite island_8_c;
    [SerializeField] Sprite island_0_c;

    [SerializeField] Sprite island_1_t;
    [SerializeField] Sprite island_2_t;
    [SerializeField] Sprite island_3_t;
    [SerializeField] Sprite island_4_t;
    [SerializeField] Sprite island_5_t;
    [SerializeField] Sprite island_6_t;
    [SerializeField] Sprite island_7_t;
    [SerializeField] Sprite island_8_t;
    [SerializeField] Sprite island_0_t;

    [SerializeField] GameObject level_0_button;
    SaveData.LevelData levels;


    private void Awake()
    {
        levels = GamePreferencesManager.LoadLevelData();
        var lastIsland = PlayerPrefs.GetString(GamePreferencesManager.m_LastSceneKey, "Level_0");
        switch (lastIsland)
        {
            case "Island_0":
                playerIcon.transform.position = island_0_button.transform.position;
                levels.island_0 = 2;
                levels.island_1 = SetTravelable(levels.island_1);
                levels.island_2 = SetTravelable(levels.island_2);
                break;
            case "Island_1":
                playerIcon.transform.position = island_1_button.transform.position;
                levels.island_1 = 2;
                levels.island_0 = SetTravelable(levels.island_0);
                levels.island_2 = SetTravelable(levels.island_2);
                levels.island_3 = SetTravelable(levels.island_3);
                break;
            case "Island_2":
                playerIcon.transform.position = island_2_button.transform.position;
                levels.island_2 = 2;
                levels.island_0 = SetTravelable(levels.island_0);
                levels.island_1 = SetTravelable(levels.island_1);
                levels.island_6 = SetTravelable(levels.island_6);
                break;
            case "Island_3":
                playerIcon.transform.position = island_3_button.transform.position;
                levels.island_3 = 2;
                levels.island_1 = SetTravelable(levels.island_1);
                levels.island_5 = SetTravelable(levels.island_5);
                break;
            case "Island_4":
                playerIcon.transform.position = island_4_button.transform.position;
                levels.island_4 = 2;
                levels.island_2 = SetTravelable(levels.island_7);
                levels.island_2 = SetTravelable(levels.island_8);
                break;
            case "Island_5":
                playerIcon.transform.position = island_5_button.transform.position;
                levels.island_5 = 2;
                levels.island_3 = SetTravelable(levels.island_3);
                levels.island_2 = SetTravelable(levels.island_8);
                break;
            case "Island_6":
                playerIcon.transform.position = island_6_button.transform.position;
                levels.island_6 = 2;
                levels.island_2 = SetTravelable(levels.island_2);
                levels.island_7 = SetTravelable(levels.island_7);
                break;
            case "Island_7":
                playerIcon.transform.position = island_7_button.transform.position;
                levels.island_7 = 2;
                levels.island_6 = SetTravelable(levels.island_6);
                levels.island_8 = SetTravelable(levels.island_8);
                levels.island_4 = SetTravelable(levels.island_4);
                break;
            case "Island_8":
                playerIcon.transform.position = island_8_button.transform.position;
                levels.island_8 = 2;
                levels.island_5 = SetTravelable(levels.island_5);
                levels.island_7 = SetTravelable(levels.island_7);
                levels.island_4 = SetTravelable(levels.island_4);
                break;
            case "Level_0":
                playerIcon.transform.position = level_0_button.transform.position;
                levels.level_0 = 2;
                break;
            default:
                break;
        }
        SetButtonStatus(island_0_button, levels.island_0, island_0_c, island_0_t);
        SetButtonStatus(island_1_button, levels.island_1, island_1_c, island_1_t);
        SetButtonStatus(island_2_button, levels.island_2, island_2_c, island_2_t);
        SetButtonStatus(island_3_button, levels.island_3, island_3_c, island_3_t);
        SetButtonStatus(island_4_button, levels.island_4, island_4_c, island_4_t);
        SetButtonStatus(island_5_button, levels.island_5, island_5_c, island_5_t);
        SetButtonStatus(island_6_button, levels.island_6, island_6_c, island_6_t);
        SetButtonStatus(island_7_button, levels.island_7, island_7_c, island_7_t);
        SetButtonStatus(island_8_button, levels.island_8, island_8_c, island_8_t);
    }

    public void LoadLevel(string level)
    {
        GamePreferencesManager.SaveLevelData(levels);
        SceneManager.LoadScene(level);
    }

    private void SetButtonStatus(GameObject btn, int state, Sprite c_sprite, Sprite t_sprite)
    {
        Debug.Log($"{btn.name} is set to {state}");
        if(state == 0)
        {
            btn.GetComponent<Button>().enabled = false;
        }
        else if (state == 1)
        {
            btn.GetComponent<Button>().enabled = true;
            btn.GetComponent<Image>().sprite = t_sprite;
        }
        else
        {
            btn.GetComponent<Button>().enabled = false;
            btn.GetComponent<Image>().sprite = c_sprite;
        }
    }
    private int SetTravelable(int state)
    {
        if(state == 0)
        {
            return 1;
        }
        else if (state == 1)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}
