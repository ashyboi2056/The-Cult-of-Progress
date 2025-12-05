using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class UI_STANCE_SelectDropdown : DEBUGMonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    List<Stance> myAvailableStances = new List<Stance>();

    private void Awake()
    {
        myAvailableStances.Clear();

        // Load all stance ScriptableObjects from Resources/Stances
        foreach (Stance stance in Enum.GetValues(typeof(Stance))){ myAvailableStances.Add(stance); }

        PopulateDropdown();
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void PopulateDropdown()
    {
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var stance in myAvailableStances)
        {
            options.Add(stance.ToString());
        }

        dropdown.AddOptions(options);
    }

    private void OnDropdownChanged(int index)
    {
        Stance selectedStance = myAvailableStances[index];
        if (debug){ Debug.Log("Selected Stance: " + selectedStance.ToString()); }
    }
}