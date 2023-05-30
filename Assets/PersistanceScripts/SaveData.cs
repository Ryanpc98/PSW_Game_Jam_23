using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct PlayerData
    {
        public float m_Health;
        public float m_XP;
        public int m_level;
        public WeaponReference.rangedWeapon m_rangedWeapon;
        public WeaponReference.meleeWeapon m_meleeWeapon;
        public string m_lastScene;
    }

    //Values
    //0 = Untouched, untavelable
    //1 = Travelable, untouched
    //2 = Completed
    [System.Serializable]
    public struct LevelData
    {
        public int island_0;
        public int island_1;
        public int island_2;
        public int island_3;
        public int island_4;
        public int island_5;
        public int island_6;
        public int island_7;
        public int island_8;
        public int level_0;

    }

    public bool m_gameOver;
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}