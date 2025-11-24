using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Ritual", menuName = "CULT/RITUAL++", order = 1)]
public class soDATA_RITUAL : ScriptableObject
{
    // INTERNAL DATA //

    [SerializeField] [Label("Ritual Name")] private string soDATA_ritualName = "NO NAME";
    
    [Space(10)]

    [SerializeField][Label("Required Items")] private soDATA_ITEM[] soDATA_reqItems = new soDATA_ITEM[1];

    [Space(10)]

    [SerializeField] [Label("Required NPCs")] private NPC_IDEnums[] soDATA_reqNPCs = new NPC_IDEnums[1];

    [Space(10)]

    [SerializeField] [Label("On Performed Effects")] private soDATA_EFFECT[] soDATA_OnPerformedEFFECTS;


    ////
    
    // POINTERS //

    public string STAT_ritualName => soDATA_ritualName;
    public soDATA_ITEM[] STAT_reqItems => soDATA_reqItems;
    public NPC_IDEnums[] STAT_reqNPCs => soDATA_reqNPCs;
    public soDATA_EFFECT[] STAT_OnPerformedEFFECTS => soDATA_OnPerformedEFFECTS;

    ////
    
    public void Performed()
    {
        foreach (var effect in soDATA_OnPerformedEFFECTS){ effect.Apply(); }
    }
}