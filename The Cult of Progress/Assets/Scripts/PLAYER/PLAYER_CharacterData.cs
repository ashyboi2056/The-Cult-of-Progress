using NaughtyAttributes;
using UnityEngine;

public class PLAYER_CharacterData : DEBUGMonoBehaviour
{
    // CHARACTER DATA AS IN MAIN SCENE FOR LOCAL REFERENCE ONLY

    public PLAYER_PERSISTANT_DATA player_persistant_data;

    public string pcName = "NO NAME";

    public soDATA_CULT_Stats chosenCult;

    [Header("WARNING!!!: SLIDER MAX RANGE IS MANUALLY SET IN THIS SCRIPT...")]
    [Header("...IT DOES NOT AUTOMATICALLY SYNC WITH MAX IN RULES!")]
    [Space(5)]
    [Range(0, RULES.RULE_STATS_maxIntelligence)]
    public int intelligence = RULES.RULE_STATS_startingIntelligence;
    [Range(0, RULES.RULE_STATS_maxCharisma)]
    public int charisma = RULES.RULE_STATS_startingCharisma;
    [Range(0, RULES.RULE_STATS_maxStrength)]
    public int strength = RULES.RULE_STATS_startingStrength;
    
    // HIDDEN STATS //
    
    public bool QueryShowHiddenStats = false;

    [ShowIf("QueryShowHiddenStats")]
    [Range(0, RULES.RULE_STATS_maxLuck)]
    public int luck = RULES.RULE_STATS_startingLuck;
    [ShowIf("QueryShowHiddenStats")]
    [Range(0, RULES.RULE_STATS_maxMagicka)]
    public int magicka = RULES.RULE_STATS_startingMagicka;

    ////
    
    [Space(10)]

    // STARTING ACCESSORY DATA CARRIER //
    public soDATA_ITEM_Accessory startingACC;

    ////
    
    [Button("Reset Character Data to System Default!")]
    private void ResetCharacterData()
    {
        pcName = "NO NAME";

        intelligence = RULES.RULE_STATS_startingIntelligence;
        charisma = RULES.RULE_STATS_startingCharisma;
        strength = RULES.RULE_STATS_startingStrength;

        luck = RULES.RULE_STATS_startingLuck;
        magicka = RULES.RULE_STATS_startingMagicka;

        QueryShowHiddenStats = false;
    }

    void Start()
    {
        player_persistant_data = FindFirstObjectByType<PlayerPersistantDataContainer>().data;

        SetCharacterData();

        chosenCult = FindFirstObjectByType<PlayerPersistantDataContainer>().cultDataCarrier;
    }

    void SetCharacterData()
    {
        pcName = player_persistant_data.characterName;

        intelligence = player_persistant_data.intelligence;
        charisma = player_persistant_data.charisma;
        strength = player_persistant_data.strength;

        GetComponent<PLAYER_Inventory>().PickUpItem(player_persistant_data.startingACC);
        
        //
        //
    }
}