using UnityEngine;

[CreateAssetMenu]
public class NPC_Stats : ScriptableObject
{
    public string STAT_characterName;
    public NPC_IDEnums STAT_id;

    [Space(10)]

    public MAP_LocationEnums STAT_home;
    
    [Space(10)]

    public int STAT_willpower = 1;
    public int STAT_devotionlessness = 1;
    public int STAT_innocence = 1;

    [Space(10)]

    public int STAT_startRESDomination = 0;
    public int STAT_startRESEnthrallment = 0;
    public int STAT_startRESCorruption = 0;

    [Space(10)]

    public NPC_IDEnums[] STAT_autoJoinList;
}