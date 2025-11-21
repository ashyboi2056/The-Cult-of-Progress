using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Usable Item", menuName = "ITEM_USE++")]
public class soDATA_ITEM_Usable : soDATA_ITEM
{
    // INTERNAL DATA //

    [SerializeField] [Label("Is Item Consumable?")] private bool soDATA_isConsumable = true;

    [Space(10)]
    
    [SerializeField] [Label("On Use Effects")] private soDATA_EFFECT[] soDATA_OnUseEFFECTS;

    ////

    // POINTERS //

    public bool STAT_isConsumable => soDATA_isConsumable;

    public soDATA_EFFECT[] STAT_OnUseEFFECTS => soDATA_OnUseEFFECTS;

    ////
    
    public void Use()
    {
        foreach (var effect in soDATA_OnUseEFFECTS){ effect.Apply(); }
    }
}