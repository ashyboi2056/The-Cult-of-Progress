using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Accessory", menuName = "ITEM_ACC++")]
public class soDATA_ITEM_Accessory : soDATA_ITEM
{
    // INTERNAL DATA //

    [SerializeField] [Label("On Equip Effects")] private soDATA_EFFECT[] soDATA_OnEquipEFFECTS;

    ////
    
    // POINTERS //

    public soDATA_EFFECT[] STAT_OnEquipEFFECTS => soDATA_OnEquipEFFECTS;

    ////
}