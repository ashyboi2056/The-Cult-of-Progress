using UnityEngine;
using System;
using NaughtyAttributes;

public class NPC_RuntimeData : DEBUGMonoBehaviour
{
    // LOCAL DATA ONLY //
    // SYNCING DATA ACROSS NETWORK MEANS PLAYERS CAN BENEFIT FROM OPPONENTS CORRUPTION ATTEMPTS //

    [SerializeField, Expandable]
    [OnValueChanged("OnDataChange")]
    private soDATA_NPC_Stats data;

    [SerializeField, ReadOnly] public bool hasBeenMet = false;

    private void OnDataChange()
    {
        Setup();
    }

    private void Start()
    {
        hasBeenMet = false;
    }

    private void Awake()
    {
        SlaveMethods();

        Setup();
    }

    public soDATA_NPC_Stats GetData()
    {
        return data;
    }

    private void SlaveMethods()
    {
        OnIntelligenceGetAct += OnIntelligenceGet;
        OnIntelligenceSetAct += OnIntelligenceSet;

        OnCharismaGetAct += OnCharismaGet;
        OnCharismaSetAct += OnCharismaSet;

        OnStrengthGetAct += OnStrengthGet;
        OnStrengthSetAct += OnStrengthSet;

        OnInnocenceGetAct += OnInnocenceGet;
        OnInnocenceSetAct += OnInnocenceSet;

        OnDetachmentGetAct += OnDetachmentGet;
        OnDetachmentSetAct += OnDetachmentSet;

        OnWillpowerGetAct += OnWillpowerGet;
        OnWillpowerSetAct += OnWillpowerSet;

        OnRESCorruptionGetAct += OnRESCorruptionGet;
        OnRESCorruptionSetAct += OnRESCorruptionSet;

        OnRESEnthrallmentGetAct += OnRESEnthrallmentGet;
        OnRESEnthrallmentSetAct += OnRESEnthrallmentSet;

        OnRESDominationGetAct += OnRESDominationGet;
        OnRESDominationSetAct += OnRESDominationSet;
    }

    [Button("Reset Character!")]
    private void Setup()
    {
        DATA_intelligence = data.STAT_intelligence;
        DATA_charisma = data.STAT_charisma;
        DATA_strength = data.STAT_strength;

        DATA_innocence = data.STAT_innocence;
        DATA_detachment = data.STAT_detachment;
        DATA_willpower = data.STAT_willpower;

        DATA_RESCorruption = data.STAT_startRESCorruption;
        DATA_RESEnthrallment = data.STAT_startRESEnthrallment;
        DATA_RESDomination = data.STAT_startRESDomination;
    }

    #region INTELLIGENCE
    // INTELLIGENCE //
    public Action OnIntelligenceGetAct;
    public Action OnIntelligenceSetAct;
    
    //Actual Data visible in Inspector
    [Space(10)]
    [Range(0,RULES.RULE_STATS_maxIntelligence)]
    [SerializeField] [Label("Intelligence")] private int DATA_intelligence;

    //Public Facing Var
    [HideInInspector] public int intelligence
    {
        get
        {
            OnIntelligenceGetAct?.Invoke();
            return DATA_intelligence;
        }
        set
        {
            DATA_intelligence = value;
            OnIntelligenceSetAct?.Invoke();
        }
    }
    
    private void OnIntelligenceGet()
    {
    
    }
    
    private void OnIntelligenceSet()
    {
    
    }

    ////
    #endregion

    #region CHARISMA
    // CHARISMA //
    public Action OnCharismaGetAct;
    public Action OnCharismaSetAct;
    
    //Actual Data visible in Inspector
    [Range(0,RULES.RULE_STATS_maxCharisma)]
    [SerializeField] [Label("Charisma")] private int DATA_charisma;

    //Public Facing Var
    [HideInInspector] public int charisma
    {
        get
        {
            OnCharismaGetAct?.Invoke();
            return DATA_charisma;
        }
        set
        {
            DATA_charisma = value;
            OnCharismaSetAct?.Invoke();
        }
    }
    
    private void OnCharismaGet()
    {
    
    }
    
    private void OnCharismaSet()
    {
    
    }

    ////
    #endregion

    #region STRENGTH
    // STRENGTH //
    public Action OnStrengthGetAct;
    public Action OnStrengthSetAct;
    
    //Actual Data visible in Inspector
    [Range(0,RULES.RULE_STATS_maxStrength)]
    [SerializeField] [Label("Strength")] private int DATA_strength;

    //Public Facing Var
    [HideInInspector] public int strength
    {
        get
        {
            OnStrengthGetAct?.Invoke();
            return DATA_strength;
        }
        set
        {
            DATA_strength = value;
            OnStrengthSetAct?.Invoke();
        }
    }
    
    private void OnStrengthGet()
    {
    
    }
    
    private void OnStrengthSet()
    {
    
    }

    ////
    #endregion

    #region INNOCENCE
    // INNOCENCE //
    public Action OnInnocenceGetAct;
    public Action OnInnocenceSetAct;
    
    //Actual Data visible in Inspector
    [Space(10)]
    [SerializeField] [Label("Innocence")] private int DATA_innocence;

    //Public Facing Var
    [HideInInspector] public int innocence
    {
        get
        {
            OnInnocenceGetAct?.Invoke();
            return DATA_innocence;
        }
        set
        {
            DATA_innocence = value;
            OnInnocenceSetAct?.Invoke();
        }
    }
    
    private void OnInnocenceGet()
    {
    
    }
    
    private void OnInnocenceSet()
    {
    
    }

    ////
    #endregion

    #region DETACHMENT
    // DETACHMENT //
    public Action OnDetachmentGetAct;
    public Action OnDetachmentSetAct;
    
    //Actual Data visible in Inspector
    [SerializeField] [Label("Detachment")] private int DATA_detachment;

    //Public Facing Var
    [HideInInspector] public int detachment
    {
        get
        {
            OnDetachmentGetAct?.Invoke();
            return DATA_detachment;
        }
        set
        {
            DATA_detachment = value;
            OnDetachmentSetAct?.Invoke();
        }
    }
    
    private void OnDetachmentGet()
    {
    
    }
    
    private void OnDetachmentSet()
    {
    
    }

    ////
    #endregion

    #region WILLPOWER
    // WILLPOWER //
    public Action OnWillpowerGetAct;
    public Action OnWillpowerSetAct;
    
    //Actual Data visible in Inspector
    [SerializeField] [Label("Willpower")] private int DATA_willpower;

    //Public Facing Var
    [HideInInspector] public int willpower
    {
        get
        {
            OnWillpowerGetAct?.Invoke();
            return DATA_willpower;
        }
        set
        {
            DATA_willpower = value;
            OnWillpowerSetAct?.Invoke();
        }
    }
    
    private void OnWillpowerGet()
    {
    
    }
    
    private void OnWillpowerSet()
    {
    
    }

    ////
    #endregion

    #region RES To CORRUPTION
    // RESCORRUPTION //
    public Action OnRESCorruptionGetAct;
    public Action OnRESCorruptionSetAct;
    
    //Actual Data visible in Inspector
    [Space(10)]
    [SerializeField] [Label("RESCorruption")] private int DATA_RESCorruption;

    //Public Facing Var
    [HideInInspector] public int RESCorruption
    {
        get
        {
            OnRESCorruptionGetAct?.Invoke();
            return DATA_RESCorruption;
        }
        set
        {
            DATA_RESCorruption = value;
            OnRESCorruptionSetAct?.Invoke();
        }
    }
    
    private void OnRESCorruptionGet()
    {
    
    }
    
    private void OnRESCorruptionSet()
    {
    
    }

    ////
    #endregion

    #region RES To ENTHRALLMENT
    // RESENTHRALLMENT //
    public Action OnRESEnthrallmentGetAct;
    public Action OnRESEnthrallmentSetAct;
    
    //Actual Data visible in Inspector
    [SerializeField] [Label("RESEnthrallment")] private int DATA_RESEnthrallment;

    //Public Facing Var
    [HideInInspector] public int RESEnthrallment
    {
        get
        {
            OnRESEnthrallmentGetAct?.Invoke();
            return DATA_RESEnthrallment;
        }
        set
        {
            DATA_RESEnthrallment = value;
            OnRESEnthrallmentSetAct?.Invoke();
        }
    }
    
    private void OnRESEnthrallmentGet()
    {
    
    }
    
    private void OnRESEnthrallmentSet()
    {
    
    }

    ////
    #endregion

    #region RES To DOMINATION
    // RESDOMINATION //
    public Action OnRESDominationGetAct;
    public Action OnRESDominationSetAct;
    
    //Actual Data visible in Inspector
    [SerializeField] [Label("RESDomination")] private int DATA_RESDomination;

    //Public Facing Var
    [HideInInspector] public int RESDomination
    {
        get
        {
            OnRESDominationGetAct?.Invoke();
            return DATA_RESDomination;
        }
        set
        {
            DATA_RESDomination = value;
            OnRESDominationSetAct?.Invoke();
        }
    }
    
    private void OnRESDominationGet()
    {
    
    }
    
    private void OnRESDominationSet()
    {
    
    }

    ////
    #endregion
}