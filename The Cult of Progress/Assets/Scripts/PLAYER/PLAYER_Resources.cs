using TMPro;
using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class PLAYER_Resources : DEBUGMonoBehaviour
{
    [SerializeField] private GenericDictionary<Resource, int> resources;

    [SerializeField] private TextMeshProUGUI booksCounter;
    [SerializeField] private TextMeshProUGUI foodCounter;
    [SerializeField] private TextMeshProUGUI metalCounter;
    [SerializeField] private TextMeshProUGUI goldCounter;
    [SerializeField] private TextMeshProUGUI soulsCounter;

    [ReadOnly, SerializeField] private int Books;
    [ReadOnly, SerializeField] private int Food;
    [ReadOnly, SerializeField] private int Metal;
    [ReadOnly, SerializeField] private int Gold;
    [ReadOnly, SerializeField] private int Souls;

    [Space(10)]

    [ShowIf("debug")]
    [SerializeField] private bool DEBUGUnlimitedPayment;

    public void Awake()
    {
        Setup();
    }

    [Button]
    private void Setup()
    {
        ResetToStartingResources();

        UpdateCounters();
    }

    private void ResetToStartingResources()
    {
        resources = new GenericDictionary<Resource, int>();

        foreach (var kvp in RULES.RULE_ECONOMY_startingResources)
        {
            resources[kvp.Key] = kvp.Value;
        }
    }

    public void AddResource(Resource type, int value)
    {
        if (value <= 0) { return; }

        resources[type] += value;
        UpdateCounters();
    }

    public void TakeResource(Resource type, int value)
    {
        if (value < 0) { return; }
        if (!QueryHasResource(type, value)) { return; }

        resources[type] -= value;
        UpdateCounters();
    }

    public void TakeResources(GenericDictionary<Resource, int> reqResources)
    {
        if (DEBUGUnlimitedPayment) { return; }

        foreach (Resource key in reqResources.Keys)
        {
            if (QueryHasResource(key, reqResources[key])) { TakeResource(key, reqResources[key]); }
            else { Debug.Log("CRITICAL ERROR at: " + name + ". Resources Taken without Payment!"); }

            continue;
        }
    }

    public void SetResource(Resource type, int value)
    {
        if (value < 0) { return; }

        resources[type] = value;
        UpdateCounters();
    }

    public bool QueryHasResource(Resource type, int value)
    {
        if (DEBUGUnlimitedPayment) { return true; }

        if (resources[type] < value) { return false; }

        return true;
    }

    public bool QueryHasResources(GenericDictionary<Resource, int> reqResources)
    {
        if (DEBUGUnlimitedPayment) { return true; }

        foreach (Resource key in reqResources.Keys)
        {
            if (!QueryHasResource(key, reqResources[key])) { return false; }

            continue;
        }
        return true;
    }

    private void UpdateCounters()
    {
        Books = resources[Resource.Books];
        Food = resources[Resource.Food];
        Metal = resources[Resource.Metal];
        Gold = resources[Resource.Gold];
        Souls = resources[Resource.Souls];

        booksCounter.text = $"Books: {Books}";
        foodCounter.text = $"Food: {Food}";
        metalCounter.text = $"Metal: {Metal}";
        goldCounter.text = $"Gold: {Gold}";
        soulsCounter.text = $"Souls: {Souls}";
    }

    // DEBUG TOOLS //

    [Button]
    [ShowIf("debug")]
    private void DEBUGGiveOneOfAllResources()
    {
        // Assuming resources is Dictionary<string, int>
        var keys = new List<Resource>(resources.Keys); // Copy keys to avoid modifying during iteration
        foreach (var key in keys)
        {
            resources[key] += 1;
        }

        UpdateCounters();
    }

    [Button]
    [ShowIf("debug")]
    private void DEBUGTakeOneOfAllResources()
    {
        // Assuming resources is Dictionary<string, int>
        var keys = new List<Resource>(resources.Keys); // Copy keys to avoid modifying during iteration
        foreach (var key in keys)
        {
            resources[key] -= 1;
        }

        UpdateCounters();
    }

    protected override void DEBUGResetDebugOptions()
    {
        base.DEBUGResetDebugOptions();

        DEBUGUnlimitedPayment = false;
    }

    ////
}