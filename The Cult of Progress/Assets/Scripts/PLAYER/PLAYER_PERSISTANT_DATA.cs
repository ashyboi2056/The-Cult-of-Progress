using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class PLAYER_PERSISTANT_DATA {
    public string playerName;
    public string characterName;

    public soDATA_ITEM_Accessory startingACC;

    public int intelligence;
    public int charisma;
    public int strength;
}

public static class SaveManager {
    private static string path = Application.persistentDataPath + "/save.json";

    public static void SaveData(PLAYER_PERSISTANT_DATA data) {
        string json = JsonUtility.ToJson(data, true); // pretty print
        File.WriteAllText(path, json);
        Debug.Log("Data saved to: " + path);
    }

    public static PLAYER_PERSISTANT_DATA LoadData() {
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            PLAYER_PERSISTANT_DATA loadedData = JsonUtility.FromJson<PLAYER_PERSISTANT_DATA>(json);
            Debug.Log("Data loaded from: " + path);
            return loadedData;
        }
        Debug.LogWarning("No save file found, returning default data.");
        string pathFind = Application.persistentDataPath + "/save.json";
        Debug.Log("Persistent data path: " + Application.persistentDataPath);
        Debug.Log("Full save path: " + pathFind);
        return new PLAYER_PERSISTANT_DATA(); // default
    }
}