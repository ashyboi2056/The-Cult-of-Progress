using UnityEngine;
using NaughtyAttributes;
using System.Linq;

[CreateAssetMenu(fileName = "New Ritual", menuName = "CULT/RITUAL++", order = 1)]
public class soDATA_RITUAL : ScriptableObject
{
    // INTERNAL DATA //

    [SerializeField][Label("Ritual Name")] private string soDATA_ritualName = "NO NAME";

    [Space(10)]

    [Range(1,600)]
    [SerializeField][Label("Time to Complete")] private float soDATA_ritualCompletionTime = 1;

    [Space(10)]

    [SerializeField][Label("Focal Item")] private soDATA_ITEM soDATA_focalItem;
    [SerializeField][Label("Focal Bound")] private NPC_IDEnums[] soDATA_focalBound = new NPC_IDEnums[2];
    
    [Space(10)]

    [SerializeField][Label("Required Items")] private soDATA_ITEM[] soDATA_reqItems = new soDATA_ITEM[1];
    [SerializeField][Label("Required Bound")] private NPC_IDEnums[] soDATA_reqBound = new NPC_IDEnums[1];

    [Space(10)]

    [SerializeField][Label("🚩On Started Effects")] private soDATA_EFFECT[] soDATA_OnStartedEFFECTS;
    [SerializeField][Label("❌On Failed Effects")] private soDATA_EFFECT[] soDATA_OnFailedEFFECTS;
    [SerializeField][Label("✅On Performed Effects")] private soDATA_EFFECT[] soDATA_OnPerformedEFFECTS;


    ////
    
    // POINTERS //

    public string STAT_ritualName => soDATA_ritualName;

    public float STAT_ritualCompletionTime => soDATA_ritualCompletionTime;

    public soDATA_ITEM STAT_focalItem => soDATA_focalItem;
    public NPC_IDEnums[] STAT_focalBound => soDATA_focalBound;

    public soDATA_ITEM[] STAT_reqItems => soDATA_reqItems;
    public NPC_IDEnums[] STAT_reqBound => soDATA_reqBound;

    public soDATA_EFFECT[] STAT_OnStartedEFFECTS => soDATA_OnStartedEFFECTS;
    public soDATA_EFFECT[] STAT_OnFailedEFFECTS => soDATA_OnFailedEFFECTS;
    public soDATA_EFFECT[] STAT_OnPerformedEFFECTS => soDATA_OnPerformedEFFECTS;

    ////
    
    public void Performed()
    {
        foreach (var effect in soDATA_OnPerformedEFFECTS){ effect.Apply(); }
    }

    public void Interrupted()
    {
        foreach (var effect in soDATA_OnFailedEFFECTS){ effect.Apply(); }
    }

    public void Started()
    {
        foreach (var effect in soDATA_OnStartedEFFECTS){ effect.Apply(); }
    }

    public bool QueryStartRitual(NPC_IDEnums[] bound, soDATA_ITEM[] items)
    {
        foreach (NPC_IDEnums focalBound in soDATA_focalBound)
        {
            if (!bound.Contains(focalBound)){ return false; }
        }
        foreach (NPC_IDEnums reqBound in soDATA_reqBound)
        {
            if (!bound.Contains(reqBound)){ return false; }
        }
        foreach (soDATA_ITEM reqItem in soDATA_reqItems)
        {
            if (!items.Contains(reqItem)){ return false; }
        }

        if (!items.Contains(STAT_focalItem)){ return false; }

        return true;
    }
}