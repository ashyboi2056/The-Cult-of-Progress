using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC++")]
public class soDATA_NPC_Stats : ScriptableObject
{
    // INTERNAL DATA //

    [SerializeField] [Label("soDATA_characterName")] private string soDATA_characterName = "NO NAME";
    [SerializeField] [Label("soDATA_id")] private NPC_IDEnums soDATA_id = NPC_IDEnums.None;

    [Space(10)]

    [SerializeField] [Label("soDATA_home")] private MAP_LocationEnums soDATA_home = MAP_LocationEnums.None;

    [Space(10)]

    [Range(0, RULES.RULE_STATS_maxIntelligence)]
    [SerializeField] [Label("soDATA_intelligence")] private int soDATA_intelligence = 1;
    [Range(0, RULES.RULE_STATS_maxCharisma)]
    [SerializeField] [Label("soDATA_charisma")] private int soDATA_charisma = 1;
    [Range(0, RULES.RULE_STATS_maxStrength)]
    [SerializeField] [Label("soDATA_strength")] private int soDATA_strength = 1;

    [Space(10)]

    [SerializeField] [Label("soDATA_innocence")] private int soDATA_innocence = 1;
    [SerializeField] [Label("soDATA_detachment")] private int soDATA_detachment = 1;
    [SerializeField] [Label("soDATA_willpower")] private int soDATA_willpower = 1;

    [Space(10)]

    [SerializeField] [Label("soDATA_startRESCorruption")] private int soDATA_startRESCorruption = 0;
    [SerializeField] [Label("soDATA_startRESEnthrallment")] private int soDATA_startRESEnthrallment = 0;
    [SerializeField] [Label("soDATA_startRESDomination")] private int soDATA_startRESDomination = 0;

    [Space(10)]

    [SerializeField] [Label("soDATA_autoJoinList")] private NPC_IDEnums[] soDATA_autoJoinList;

    [Space(10)]

    [SerializeField] [Label("soDATA_conversionAttemptRefreshTime")] private float soDATA_conversionAttemptRefreshTime = 1;

    ////
    

    
    // POINTERS //

    public string STAT_characterName => soDATA_characterName;
    public NPC_IDEnums STAT_id => soDATA_id;

    public MAP_LocationEnums STAT_home => soDATA_home;

    public int STAT_intelligence => soDATA_intelligence;
    public int STAT_charisma => soDATA_charisma;
    public int STAT_strength => soDATA_strength;
    
    public int STAT_innocence => soDATA_innocence;
    public int STAT_detachment => soDATA_detachment;
    public int STAT_willpower => soDATA_willpower;
    
    public int STAT_startRESCorruption => soDATA_startRESCorruption;
    public int STAT_startRESEnthrallment => soDATA_startRESEnthrallment;
    public int STAT_startRESDomination => soDATA_startRESDomination;

    public NPC_IDEnums[] STAT_autoJoinList => soDATA_autoJoinList;

    public float STAT_conversionAttemptRefreshTime => soDATA_conversionAttemptRefreshTime;

    ////
}