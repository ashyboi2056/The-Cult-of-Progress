using UnityEngine;

[ExecuteAlways]
public class UI_CraftingStationWindowHandler : DEBUGMonoBehaviour
{
    [SerializeField] private GameObject itemCrafterUIPrefab;
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform itemCrafterUIParentTransform;
    [SerializeField] private UI_Panel myUIPanelScript;

    private MAP_CraftingStation craftingStation;

    private void OpenPanel()
    {
        if (debug) { Debug.Log("Opening Panel at: " + name); }
        if (craftingStation == null)
        {
            if (debug) { Debug.Log("ERROR: Crafting Station Panel Cannot Open without Station Specified. Error At: " + name); }
            return;
        }

        Setup();

        panel.SetActive(true);

        myUIPanelScript.isPanelOpen = true;
    }

    public void ClosePanel()
    {
        if (debug) { Debug.Log("Closing Panel at: " + name); }

        panel.SetActive(false);

        SetDown();

        myUIPanelScript.isPanelOpen = false;
    }

    private void SetDown()
    {
        //Scrub old Data and UI
        KillAllItemCrafterUI();
    }

    private void Setup()
    {
        //create new UI and upload Data
        foreach (soDATA_ITEM item in craftingStation.craftables)
        {
            if (item == null){ if (debug){ Debug.Log("Empty item at: " + name); } continue; } //Empty Item Slot Chcek

            SpawnItemCrafterUI(item);
        }
    }

    public void SetCraftingStation(MAP_CraftingStation newCraftingStation)
    {
        SetDown();

        craftingStation = newCraftingStation;

        OpenPanel();
    }

    private void SpawnItemCrafterUI(soDATA_ITEM item)
    {
        UI_ItemCrafter newItemCrafterUI = Instantiate(itemCrafterUIPrefab, itemCrafterUIParentTransform).GetComponent<UI_ItemCrafter>();

        newItemCrafterUI.SetItem(item);
    }

    private void KillAllItemCrafterUI()
    {
        foreach (Transform child in itemCrafterUIParentTransform)
        {
            Destroy(child.gameObject);
        }
    }
}