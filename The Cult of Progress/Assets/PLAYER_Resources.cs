using TMPro;
using UnityEngine;
using NaughtyAttributes;

public class PLAYER_Resources : DEBUGMonoBehaviour
{
    private GenericDictionary<Resource,int> resources = RULES.RULE_ECONOMY_startingResources;

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

    public void Awake()
    {
        Setup();
    }

    [Button]
    private void Setup()
    {
        resources = RULES.RULE_ECONOMY_startingResources;
        UpdateCounters();
    }

    public void AddResource(Resource type, int value)
    {
        if (value <= 0){ return; }

        resources[type] += value;
        UpdateCounters();
    }

    public void TakeResource(Resource type, int value)
    {
        if (value < 0){ return; }
        if (!QueryHasResource(type, value)){ return; }

        resources[type] -= value;
        UpdateCounters();
    }

    public void SetResource(Resource type, int value)
    {
        if (value < 0){ return; }

        resources[type] = value;
        UpdateCounters();
    }

    public bool QueryHasResource(Resource type, int value)
    {
        if (resources[type] < value){ return false; }

        return true;
    }

    private void UpdateCounters()
    {
        Books = resources[Resource.Books];
        Food = resources[Resource.Food];
        Metal = resources[Resource.Metal];
        Gold = resources[Resource.Metal];
        Souls = resources[Resource.Souls];

        booksCounter.text = $"Books: {Books}";
        foodCounter.text = $"Food: {Food}";
        metalCounter.text = $"Metal: {Metal}";
        goldCounter.text = $"Gold: {Gold}";
        soulsCounter.text = $"Souls: {Souls}";
    }
}