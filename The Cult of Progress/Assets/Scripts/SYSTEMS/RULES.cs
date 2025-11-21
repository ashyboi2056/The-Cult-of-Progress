public class RULES
{
    // CONVERSION SYSTEM RULES //

    public const int RULE_CONVERSION_defaultAttempts = 1;

    ////

    // HEALTH SYSTEM RULES //

    public const int RULE_HP_headMaxHP = 3;
    public const int RULE_HP_bodyMaxHP = 3;
    public const int RULE_HP_armsMaxHP = 3;
    public const int RULE_HP_legsMaxHP = 3;

    public const int RULE_HP_soulMaxHP = 5;

    ////
    
    // STATS RULES //

    public const int RULE_STATS_startingIntelligence = 1;
    public const int RULE_STATS_maxIntelligence = 5;
    public const int RULE_STATS_startingCharisma = 1;
    public const int RULE_STATS_maxCharisma = 5;
    public const int RULE_STATS_startingStrength = 1;
    public const int RULE_STATS_maxStrength = 5;

    public const int RULE_STATS_startingLuck = 1;
    public const int RULE_STATS_maxLuck = 5;
    public const int RULE_STATS_startingMagicka = 0;
    public const int RULE_STATS_maxMagicka = 5;

    ////
    
    // ECONOMY RULES //

    public static GenericDictionary<Resource,int> RULE_ECONOMY_startingResources = new GenericDictionary<Resource, int>()
    {
        {Resource.Books, 0},
        {Resource.Food, 0},
        {Resource.Gold, 0},
        {Resource.Metal, 0},
        {Resource.Souls, 0}
    };

    ////
}