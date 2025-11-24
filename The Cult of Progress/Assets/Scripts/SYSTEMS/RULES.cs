public class RULES
{
    /// <summary>
    /// // STATIC VARS ORDER IN THIS SCRIPT MATTERS!!! ACCESSING LOWER VARS IS IMPOSSIBLE FOR HIGHER VARS AS THEY HAVE NOT 
    // LOADED YET IF THEY ARE BOTH STATIC AND USED IN INITIALIZATION
    /// </summary>

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

    public static readonly GenericDictionary<Resource,int> RULE_ECONOMY_startingResources = new GenericDictionary<Resource, int>()
    {
        {Resource.Books, 0},
        {Resource.Food, 0},
        {Resource.Gold, 0},
        {Resource.Metal, 0},
        {Resource.Souls, 0}
    };

    private static float baseResourceGenerationTickAdjustment = 1000f;
    //Per Tick
    public static readonly GenericDictionary<Resource,float> RULE_ECONOMY_baseResourceGenerationRate = new GenericDictionary<Resource, float>()
    {
        {Resource.Books, 2f/baseResourceGenerationTickAdjustment},
        {Resource.Food, 2f/baseResourceGenerationTickAdjustment},
        {Resource.Gold, 2f/baseResourceGenerationTickAdjustment},
        {Resource.Metal, 2f/baseResourceGenerationTickAdjustment},
        {Resource.Souls, 1f/baseResourceGenerationTickAdjustment}
    };
    
    ////
}