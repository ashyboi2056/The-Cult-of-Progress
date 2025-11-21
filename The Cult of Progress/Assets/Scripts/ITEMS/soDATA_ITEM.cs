using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Item", menuName = "ITEM++")]
public class soDATA_ITEM  : ScriptableObject
{
    // INTERNAL DATA //

    [SerializeField] [Label("Item Name")] private string soDATA_itemName = "NO NAME";

    [Space(10)]
    
    [SerializeField] [Label("soDATA_home")] private MAP_LocationEnums soDATA_home = MAP_LocationEnums.None;

    [SerializeField] private GenericDictionary<Resource,int> soDATA_craftingReq = new GenericDictionary<Resource, int>()
    {
        {Resource.Books, 0},
        {Resource.Food, 0},
        {Resource.Gold, 0},
        {Resource.Metal, 0},
        {Resource.Souls, 0}
    };

    ////
    
    // POINTERS //

    public string STAT_itemName => soDATA_itemName;

    public MAP_LocationEnums STAT_home => soDATA_home;

    public GenericDictionary<Resource,int> STAT_craftingReq => soDATA_craftingReq;

    ////
}