using System;
using UnityEngine;

public class MAP_CraftingStation : DEBUGMonoBehaviour
{
    public soDATA_ITEM[] craftables;
    
    private UI_CraftingStationWindowHandler uiHandler;

    void Awake()
    {
        uiHandler = FindFirstObjectByType<UI_CraftingStationWindowHandler>();
    }

    public void SetupUI()
    {
        uiHandler.SetCraftingStation(this);
    }
}