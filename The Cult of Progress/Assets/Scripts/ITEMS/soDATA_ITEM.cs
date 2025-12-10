using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Item", menuName = "ITEM++")]
public class soDATA_ITEM : ScriptableObject
{
    // INTERNAL DATA //

    [SerializeField, Label("Item Name")] 
    private string soDATA_itemName = "NO NAME";

    [Space(10)]
    
    [SerializeField, Label("soDATA_home")] 
    private MAP_LocationEnums soDATA_home = MAP_LocationEnums.None;

    [SerializeField] 
    private GenericDictionary<Resource, int> soDATA_craftingReq = new GenericDictionary<Resource, int>()
    {
        {Resource.Food, 0},
        {Resource.Books, 0},
        {Resource.Metal, 0},
        {Resource.Gold, 0},
        {Resource.Souls, 0}
    };

    [Space(10)]
    
    [SerializeField, Label("Item Sprite")] 
    private Sprite soDATA_sprite; // ✅ Added sprite field

    ////

    // POINTERS //

    public string STAT_itemName => soDATA_itemName;
    public MAP_LocationEnums STAT_home => soDATA_home;
    public GenericDictionary<Resource, int> STAT_craftingReq => soDATA_craftingReq;
    public Sprite STAT_sprite => soDATA_sprite; // ✅ Public getter for sprite

    ////
}
