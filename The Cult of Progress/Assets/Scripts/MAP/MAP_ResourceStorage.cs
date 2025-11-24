using NaughtyAttributes;
using UnityEngine;

public class MAP_ResourceStorage : DEBUGMonoBehaviour
{
    [SerializeField] private GenericDictionary<Resource,float> storage = new GenericDictionary<Resource, float>()
    {
        {Resource.Books, 0},
        {Resource.Food, 0},
        {Resource.Gold, 0},
        {Resource.Metal, 0},
        {Resource.Souls, 0}
    };

    public void Store(Resource type, float amount)
    {
        storage[type] += amount;
    }

    public int TakeMaxIntResource(Resource type)
    {
        //Calculate
        int amount = GetMaxIntResource(type);

        //Take
        storage[type] -= amount;

        //Return
        return amount;
    }

    public int GetMaxIntResource(Resource type)
    {
        return (int)storage[type];
    }

    public GenericDictionary<Resource,float> GetStorage()
    {
        return storage;
    }
}