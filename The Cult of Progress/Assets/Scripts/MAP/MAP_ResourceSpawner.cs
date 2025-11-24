using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class MAP_ResourceSpawner : DEBUGMonoBehaviour
{
    [Label("Spawn which resources?")]
    [OnValueChanged("Setup")]
    [SerializeField] private GenericDictionary<Resource,bool> QuerySpawnResources = new GenericDictionary<Resource, bool>()
    {
        {Resource.Books, false},
        {Resource.Food, false},
        {Resource.Gold, false},
        {Resource.Metal, false},
        {Resource.Souls, false}
    };
    [SerializeField] private MAP_ResourceStorage storage;

    [Space(10)]

    [SerializeField] private float maxPickupDistance;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI booksCounter;
    [SerializeField] private TextMeshProUGUI foodCounter;
    [SerializeField] private TextMeshProUGUI metalCounter;
    [SerializeField] private TextMeshProUGUI goldCounter;
    [SerializeField] private TextMeshProUGUI soulsCounter;

    public void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        foreach (Resource rType in QuerySpawnResources.Keys)
        {
            switch (rType)
            {
                case Resource.Books:
                    booksCounter.gameObject.SetActive(QuerySpawnResources[rType]);
                    break;
                case Resource.Food:
                    foodCounter.gameObject.SetActive(QuerySpawnResources[rType]);
                    break;
                case Resource.Metal:
                    metalCounter.gameObject.SetActive(QuerySpawnResources[rType]);
                    break;
                case Resource.Gold:
                    goldCounter.gameObject.SetActive(QuerySpawnResources[rType]);
                    break;
                case Resource.Souls:
                    soulsCounter.gameObject.SetActive(QuerySpawnResources[rType]);
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
        //Create Resources
        GenerateResources();
    }

    public void Update()
    {
        //Distribute Resources
        GameObject closestPlayer = GetClosestPlayer();

        float distance = Vector2.Distance(closestPlayer.transform.position, transform.position);

        if (distance < maxPickupDistance)
        {
            Distribute(closestPlayer.GetComponent<PLAYER_Resources>());
        }
    }

    private void GenerateResources()
    {
        foreach (Resource rType in QuerySpawnResources.Keys)
        {
            //Spawn this
            if (QuerySpawnResources[rType])
            {
                storage.Store(rType,RULES.RULE_ECONOMY_baseResourceGenerationRate[rType]);
            }
        }

        UpdateCounters();
    }

    private GameObject GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(currentPos, p.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = p;
            }
        }

        return nearest;
    }

    private void Distribute(PLAYER_Resources targetPlayer)
    {
        GenericDictionary<Resource,float> storageTempVar = new GenericDictionary<Resource, float>();

        foreach (var kvp in storage.GetStorage())
        {
            storageTempVar.Add(kvp);
        }

        foreach (Resource rType in storageTempVar.Keys)
        {
            targetPlayer.AddResource(rType,storage.TakeMaxIntResource(rType));
        }

        UpdateCounters();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxPickupDistance);
    }

    private void UpdateCounters()
    {
        booksCounter.text = $"Books: {storage.GetMaxIntResource(Resource.Books)}";
        foodCounter.text = $"Food: {storage.GetMaxIntResource(Resource.Food)}";
        metalCounter.text = $"Metal: {storage.GetMaxIntResource(Resource.Metal)}";
        goldCounter.text = $"Gold: {storage.GetMaxIntResource(Resource.Gold)}";
        soulsCounter.text = $"Souls: {storage.GetMaxIntResource(Resource.Souls)}";
    }
}