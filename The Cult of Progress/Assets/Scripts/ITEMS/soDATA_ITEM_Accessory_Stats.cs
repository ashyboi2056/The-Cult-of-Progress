using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Accessory", menuName = "ITEM_ACC++")]
public class soDATA_ITEM_Accessory : soDATA_ITEM
{
    // INTERNAL DATA //

    [SerializeField] [Label("On Equip Event")] private UnityEvent soDATA_OnEquip = new UnityEvent();

    ////
    
    // POINTERS //

    public UnityEvent STAT_OnEquip => soDATA_OnEquip;

    ////
}