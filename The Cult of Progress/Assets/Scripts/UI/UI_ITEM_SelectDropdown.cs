using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UI_ITEM_SelectDropdown : DEBUGMonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    public soDATA_ITEM[] myAvailableItems;

    private void Awake()
    {
        PopulateDropdown();
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void PopulateDropdown()
    {
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var item in myAvailableItems)
        {
            options.Add(item.STAT_itemName);
        }

        dropdown.AddOptions(options);
    }

    private void OnDropdownChanged(int index)
    {
        soDATA_ITEM selectedItem = myAvailableItems[index];
        if (debug){ Debug.Log("Selected Item: " + selectedItem.STAT_itemName); }
    }
}