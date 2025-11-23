using UnityEngine;

public class MAP_CraftingStation : DEBUGMonoBehaviour
{
    public soDATA_ITEM[] craftables;

    [SerializeField] private GameObject itemDropPrefab;
    [SerializeField] private UI_CraftingStationWindowHandler uiHandler;

    public void SetupUI()
    {
        uiHandler.SetCraftingStation(this);
    }
}