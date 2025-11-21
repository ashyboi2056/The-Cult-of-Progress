using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Location", menuName = "LOCATION++")]
public class soDATA_Location : ScriptableObject
{
    // INTERNAL DATA //

    [SerializeField] [Label("Location ID")] 
    private MAP_LocationEnums soDATA_locationID = MAP_LocationEnums.None;

    [Space(10)]

    [SerializeField] [Label("Residents")] 
    private NPC_IDEnums[] soDATA_residents = new NPC_IDEnums[0];

    [SerializeField] [Label("Key Resource")] 
    private Resource soDATA_resource = Resource.None;

    [SerializeField] [Label("Craftables")] 
    private soDATA_ITEM[] soDATA_craftables = new soDATA_ITEM[0];

    ////

    // POINTERS //

    public MAP_LocationEnums STAT_locationID => soDATA_locationID;

    public NPC_IDEnums[] STAT_residents => soDATA_residents;

    public Resource STAT_resource => soDATA_resource;

    public soDATA_ITEM[] STAT_craftables => soDATA_craftables;

    ////
}