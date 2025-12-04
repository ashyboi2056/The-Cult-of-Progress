using UnityEngine;

public class UpdatePlayerPersistantData : DEBUGMonoBehaviour
{
    [SerializeField] private PlayerPersistantDataContainer dataContainer;

    void Awake()
    {
        //Load Last Sessions Data on Awake()
        LoadData();
    }

    private void LoadData()
    {
        dataContainer.data = SaveManager.LoadData();
    }

    private void SaveData()
    {
        SaveManager.SaveData(dataContainer.data);
    }

    public void SetCharacterName(string newCharacterName)
    {
        dataContainer.data.characterName = newCharacterName;

        SaveData();
    }

    public string GetCharacterName()
    {
        return dataContainer.data.characterName;
    }

    public void SetIntelligence(int newInt)
    {
        dataContainer.data.intelligence = newInt;

        SaveData();
    }
    public void SetCharisma(int newCha)
    {
        dataContainer.data.charisma = newCha;

        SaveData();
    }
    public void SetStrength(int newStr)
    {
        dataContainer.data.strength = newStr;

        SaveData();
    }

    public void SetStartingAcc(soDATA_ITEM_Accessory item)
    {
        dataContainer.data.startingACC = item;
    }

    public void SetCultDataCarrier(soDATA_CULT_Stats cult)
    {
        dataContainer.cultDataCarrier = cult;
    }
}