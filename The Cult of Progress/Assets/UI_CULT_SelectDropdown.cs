using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UI_CULT_SelectDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    public soDATA_CULT_Stats[] myAvailableCults;

    private void Awake()
    {
        // Load all cult ScriptableObjects from Resources/Cults
        // myAvailableCults = Resources.LoadAll<soDATA_CULT_Stats>("Cults");

        PopulateDropdown();
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void PopulateDropdown()
    {
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var cult in myAvailableCults)
        {
            options.Add(cult.STAT_cultName);
        }

        dropdown.AddOptions(options);
    }

    private void OnDropdownChanged(int index)
    {
        soDATA_CULT_Stats selectedCult = myAvailableCults[index];
        Debug.Log("Selected Cult: " + selectedCult.STAT_cultName);

        // Example: apply cult logic
        // e.g. update UI, load rituals, change colors, etc.
    }
}