using NaughtyAttributes;
using UnityEngine;

public class PLAYER_CharacterData : DEBUGMonoBehaviour
{
    public string pcName = "NO NAME";

    // CHOSEN GOD = GOD;

    [Header("WARNING!!!: SLIDER MAX RANGE IS MANUALLY SET IN THIS SCRIPT...")]
    [Header("...IT DOES NOT AUTOMATICALLY SYNC WITH MAX IN RULES!")]
    [Space(5)]
    [Range(0, 5)]
    public int intelligence = RULES.RULE_STATS_startingIntelligence;
    [Range(0, 5)]
    public int strength = RULES.RULE_STATS_startingStrength;
    [Range(0, 5)]
    public int charisma = RULES.RULE_STATS_startingCharisma;

    // HIDDEN STATS //
    public bool QueryShowHiddenStats = false;
    [ShowIf("QueryShowHiddenStats")]
    [Range(0, 5)]
    public int luck = RULES.RULE_STATS_startingLuck;
    [ShowIf("QueryShowHiddenStats")]
    [Range(0, 5)]
    public int magicka = RULES.RULE_STATS_startingMagicka;

    ////

    // STARTING ACCESSORY DATA CARRIER //

    // STARTINGACC = ACC;

    ////
    
    [Button("Reset Character Data!")]
    private void ResetCharacterData()
    {
        pcName = "NO NAME";

        intelligence = RULES.RULE_STATS_startingIntelligence;
        strength = RULES.RULE_STATS_startingStrength;
        charisma = RULES.RULE_STATS_startingCharisma;

        luck = RULES.RULE_STATS_startingLuck;
        magicka = RULES.RULE_STATS_startingMagicka;

        QueryShowHiddenStats = false;
    }
}