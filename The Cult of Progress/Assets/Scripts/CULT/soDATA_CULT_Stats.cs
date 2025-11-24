using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Cult", menuName = "CULT/CULT++", order = 1)]
public class soDATA_CULT_Stats : ScriptableObject
{
    // INTERNAL DATA //

    [SerializeField] [Label("Cult Name")] private string soDATA_cultName = "NO NAME";

    [Space(10)]
    
    [Header("Faction Allies: Aim for 3!")]
    [SerializeField] [Label("Faction NPCs")] private NPC_IDEnums[] soDATA_factionNPCs = new NPC_IDEnums[3];

    [Space(10)]
    
    [SerializeField] [Label("Victory Rituals")] private soDATA_RITUAL[] soDATA_victoryRituals = new soDATA_RITUAL[1];

    ////
    
    // POINTERS //

    public string STAT_cultName => soDATA_cultName;

    public NPC_IDEnums[] STAT_factionNPCs => soDATA_factionNPCs;

    public soDATA_RITUAL[] STAT_victoryRituals => soDATA_victoryRituals;

    ////
}