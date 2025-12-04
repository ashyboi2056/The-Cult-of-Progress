using System;
using TMPro;
using UnityEngine;

public class CallUpdatePlayerPersistantData : DEBUGMonoBehaviour
{
    [SerializeField] UI_CULT_SelectDropdown LOCAL_CultDropdown;
    [SerializeField] UI_ITEM_SelectDropdown LOCAL_ItemDropdown;

    public void SetCharacterName(string newName)
    {
        FindFirstObjectByType<UpdatePlayerPersistantData>().SetCharacterName(newName);
    }

    public void SetIntelligence(Single newInt)
    {
        int var = (int)newInt;

        FindFirstObjectByType<UpdatePlayerPersistantData>().SetIntelligence(var);
    }
    public void SetCharisma(Single newCha)
    {
        int var = (int)newCha;

        FindFirstObjectByType<UpdatePlayerPersistantData>().SetCharisma(var);
    }
    public void SetStrength(Single newCha)
    {
        int var = (int)newCha;

        FindFirstObjectByType<UpdatePlayerPersistantData>().SetStrength(var);
    }

    public void SetStartingAcc(Int32 itemIndex)
    {
        soDATA_ITEM item = LOCAL_ItemDropdown.myAvailableItems[itemIndex];

        if (debug){ Debug.Log("Set Cult to: " + item.STAT_itemName); }

        FindFirstObjectByType<UpdatePlayerPersistantData>().SetStartingAcc((soDATA_ITEM_Accessory)item);
    }

    public void SetCultDataCarrier(Int32 cultIndex)
    {
        soDATA_CULT_Stats cult = LOCAL_CultDropdown.myAvailableCults[cultIndex];

        if (debug){ Debug.Log("Set Cult to: " + cult.STAT_cultName); }

        FindFirstObjectByType<UpdatePlayerPersistantData>().SetCultDataCarrier(cult);
    }
}