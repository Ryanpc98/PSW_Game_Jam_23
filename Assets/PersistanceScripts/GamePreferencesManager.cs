using UnityEngine;

public static class GamePreferencesManager
{
    public const string m_HealthKey = "health";
    public const string m_XPKey = "xp";
    public const string m_LevelKey = "level";
    public const string m_RangedWeaponKey = "range";
    public const string m_MeleeWeaponKey = "melee";
    public const string m_LastSceneKey = "scene";
    public const string m_VolumeKey = "vol";

    public const string m_Island_0_Key = "Island_0";
    public const string m_Island_1_Key = "Island_1";
    public const string m_Island_2_Key = "Island_2";
    public const string m_Island_3_Key = "Island_3";
    public const string m_Island_4_Key = "Island_4";
    public const string m_Island_5_Key = "Island_5";
    public const string m_Island_6_Key = "Island_6";
    public const string m_Island_7_Key = "Island_7";
    public const string m_Island_8_Key = "Island_8";
    public const string m_Level_0_Key = "Level_0";

    public static void SavePrefs(SaveData.PlayerData data)
    {
        PlayerPrefs.SetFloat(m_HealthKey, data.m_Health);
        PlayerPrefs.SetFloat(m_XPKey, data.m_XP);
        PlayerPrefs.SetInt(m_LevelKey, data.m_level);
        PlayerPrefs.SetInt(m_RangedWeaponKey, (int) data.m_rangedWeapon);
        PlayerPrefs.SetInt(m_MeleeWeaponKey, (int) data.m_meleeWeapon);
        PlayerPrefs.SetString(m_LastSceneKey, data.m_lastScene);
        PlayerPrefs.Save();
    }

    public static SaveData.PlayerData LoadPrefs()
    {
        var health = PlayerPrefs.GetFloat(m_HealthKey, 0);
        var XP = PlayerPrefs.GetFloat(m_XPKey, 0);
        var level = PlayerPrefs.GetInt(m_LevelKey, 0);
        var RangedWeapon = PlayerPrefs.GetInt(m_RangedWeaponKey, 0);
        var MeleeWeapon = PlayerPrefs.GetInt(m_MeleeWeaponKey, 0);
        var LastScene = PlayerPrefs.GetString(m_LastSceneKey, "");

        SaveData.PlayerData saveData = new SaveData.PlayerData
        {
            m_Health = health,
            m_XP = XP,
            m_level = level,
            m_rangedWeapon = (WeaponReference.rangedWeapon)RangedWeapon,
            m_meleeWeapon = (WeaponReference.meleeWeapon)MeleeWeapon,
            m_lastScene = LastScene
        };

        return saveData;
    }

    public static void SaveLevelData(SaveData.LevelData data)
    {
        PlayerPrefs.SetInt(m_Island_0_Key, data.island_0);
        PlayerPrefs.SetInt(m_Island_1_Key, data.island_1);
        PlayerPrefs.SetInt(m_Island_2_Key, data.island_2);
        PlayerPrefs.SetInt(m_Island_3_Key, data.island_3);
        PlayerPrefs.SetInt(m_Island_4_Key, data.island_4);
        PlayerPrefs.SetInt(m_Island_5_Key, data.island_5);
        PlayerPrefs.SetInt(m_Island_6_Key, data.island_6);
        PlayerPrefs.SetInt(m_Island_7_Key, data.island_7);
        PlayerPrefs.SetInt(m_Island_8_Key, data.island_8);
        PlayerPrefs.SetInt(m_Level_0_Key, data.level_0);

    }

    public static SaveData.LevelData LoadLevelData()
    {
    SaveData.LevelData saveData = new SaveData.LevelData
        {
            island_0 = PlayerPrefs.GetInt(m_Island_0_Key, 0),
            island_1 = PlayerPrefs.GetInt(m_Island_1_Key, 0),
            island_2 = PlayerPrefs.GetInt(m_Island_2_Key, 0),
            island_3 = PlayerPrefs.GetInt(m_Island_3_Key, 0),
            island_4 = PlayerPrefs.GetInt(m_Island_4_Key, 0),
            island_5 = PlayerPrefs.GetInt(m_Island_5_Key, 0),
            island_6 = PlayerPrefs.GetInt(m_Island_6_Key, 0),
            island_7 = PlayerPrefs.GetInt(m_Island_7_Key, 0),
            island_8 = PlayerPrefs.GetInt(m_Island_8_Key, 0),
            level_0 = PlayerPrefs.GetInt(m_Level_0_Key, 0)
    };

        return saveData;
    }
}