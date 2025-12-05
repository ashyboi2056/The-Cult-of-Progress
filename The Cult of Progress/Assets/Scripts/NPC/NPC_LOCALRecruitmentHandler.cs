using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class NPC_LOCALRecruitmentHandler : DEBUGMonoBehaviour
{
    [SerializeField]
    [OnValueChanged("UpdateAttemptsCounter")] //ONLY WORKS IN INSPECTOR
    private int conversionAttempts = RULES.RULE_CONVERSION_defaultAttempts;

    [Space(10)]

    //Actual Data visible in Inspector
    [SerializeField][Label("Runtime Data")] private NPC_RuntimeData DATA_data;

    //Public Facing Var
    [HideInInspector] public NPC_RuntimeData data
    {
        get
        {
            return DATA_data;
        }
        private set
        {
            DATA_data = value;
        }
    }

    [Space(10)]

    [SerializeField] private UnityEvent OnRecruitment;

    private UI_NPC_ConversionWindowHandler conversionWindowHandler;

    void Awake()
    {
        conversionWindowHandler = FindFirstObjectByType<UI_NPC_ConversionWindowHandler>();

        StartCoroutine(IncrementConversionAttempts());
    }

    public void OnInteract()
    {
        conversionWindowHandler.SetTargetNPC(this);
        conversionWindowHandler.OpenPanel();
    }

    private void OnDataChange()
    {
        NPCSetup();
    }

    private void OnIntelligenceChange()
    {
        if (debug){Debug.Log("Intelligence Changed!");}
    }

    private void UpdateAttemptsCounter()
    {
        conversionWindowHandler.UpdateAttemptsCounter(this);
    }

    private IEnumerator IncrementConversionAttempts()
    {
        while (true)
        {
            yield return new WaitForSeconds(data.GetData().STAT_conversionAttemptRefreshTime);
            if (conversionAttempts < RULES.RULE_CONVERSION_maxStoredAttempts){ conversionAttempts++; }
            if (debug){ Debug.Log($"conversionAttempts increased to {conversionAttempts}"); }

            conversionWindowHandler.UpdateAttemptsCounter(this);
        }
    }

    public int GetConversionAttempts()
    {
        return conversionAttempts;
    }

    [Button("Reset NPC Stats")]
    private void NPCSetup()
    {
        conversionAttempts = RULES.RULE_CONVERSION_defaultAttempts;
        UpdateAttemptsCounter();
    }

    public IEnumerator Convert(PLAYER_CharacterData playerData, Stance stance) {
        if (debug){ Debug.Log("Converting!"); }

        if (conversionAttempts == 0) {
            yield break; // no attempts left
        }
        --conversionAttempts;
        UpdateAttemptsCounter();

        int corruptionTotal = 0;
        int enthrallmentTotal = 0;
        int dominationTotal = 0;

        //Roll D6 per Stat Point for Player
        // Wait until all dice have settled
        yield return StartCoroutine(LOGIC_ConversionDiceManager.RollCoroutine(playerData.intelligence, playerData.charisma, playerData.strength, data.intelligence, data.charisma, data.strength, results => {
            if (debug){ Debug.Log($"Corruption: {results[0]}, Enthrallment: {results[1]}, Domination: {results[2]}"); }

            corruptionTotal = results[0];
            enthrallmentTotal = results[1];
            dominationTotal = results[2];
        }));
        /*
            for (int intCounter = 0; intCounter < playerData.intelligence; intCounter++)
            {
                bool success = TEMPRollCorruptionDice();
                if (success){ ++corruptionTotal; }
            }
            for (int chaCounter = 0; chaCounter < playerData.charisma; chaCounter++)
            {
                bool success = TEMPRollEnthrallmentDice();
                if (success){ ++enthrallmentTotal; }
            }
            for (int strCounter = 0; strCounter < playerData.strength; strCounter++)
            {
                bool success = TEMPRollDominationDice();
                if (success){ ++dominationTotal; }
            }
        */

        //Subtract RES
        if (corruptionTotal - data.RESCorruption <= 0){ corruptionTotal = 0; }
        else { corruptionTotal -= data.RESCorruption; }

        if (enthrallmentTotal - data.RESEnthrallment <= 0){ enthrallmentTotal = 0; }
        else { enthrallmentTotal -= data.RESEnthrallment; }

        if (dominationTotal - data.RESDomination <= 0){ dominationTotal = 0; }
        else { dominationTotal -= data.RESDomination; }

        //Subtract Result from inn, det and will
        if (data.innocence - corruptionTotal <= 0){ data.innocence = 0; }
        else { data.innocence -= corruptionTotal; }

        if (data.detachment - enthrallmentTotal <= 0){ data.detachment = 0; }
        else { data.detachment -= enthrallmentTotal; }

        if (data.willpower - dominationTotal <= 0){ data.willpower = 0; }
        else { data.willpower -= dominationTotal; }

        //CheckConverted
        CheckConverted();
    }

    private bool CheckConverted()
    {
        if (data.innocence == 0){ Corrupted(); return true; }
        else if (data.detachment == 0){ Enthralled();  return true; }
        else if (data.willpower == 0){ Dominated();  return true; }
        else{ return false; }
    }

    private void Recruited()
    {
        if (debug){ Debug.Log("<color=magenta>Recruited " + data.GetData().STAT_characterName + "!</color>"); }

        OnRecruitment.Invoke();
        UI_Panel.CloseAllPanels(); // TEMP FIX

        FindFirstObjectByType<LOCAL_PLAYER_FLAG>().GetComponent<PLAYER_NPC_RecruitedNPCs>().AddRecruitedNPC(data.GetData().STAT_id);
    }

    private void Corrupted()
    {
        Recruited();
    }

    private void Enthralled()
    {
        Recruited();
    }

    private void Dominated()
    {
        Recruited();
    }
    
    //Success or Failure
    private bool TEMPRollCorruptionDice()
    {
        int rollResult = Random.Range(1,7); //(in/ex)

        if (debug)
        {
            Debug.Log("<color=blue>Dice Roll Corruption: Roll: " + rollResult + " ToBeat: " + data.intelligence + "</color>");
            if (rollResult > data.intelligence){ Debug.Log("<color=green>Success!</color>"); }
            else { Debug.Log("<color=red>Failure!</color>"); }
        }

        if (rollResult > data.intelligence){ return true; }

        return false;
    }
    //Success or Failure
    private bool TEMPRollEnthrallmentDice()
    {
        int rollResult = Random.Range(1,7); //(in/ex)

        if (debug)
        {
            Debug.Log("<color=green>Dice Roll Enthrallment: Roll: " + rollResult + " ToBeat: " + data.charisma + "</color>");
            if (rollResult > data.charisma){ Debug.Log("<color=green>Success!</color>"); }
            else { Debug.Log("<color=red>Failure!</color>"); }
        }

        if (rollResult > data.charisma){ return true; }

        return false;
    }
    //Success or Failure
    private bool TEMPRollDominationDice()
    {
        int rollResult = Random.Range(1,7); //(in/ex)

        if (debug)
        {
            Debug.Log("<color=red>Dice Roll Domination: Roll: " + rollResult + " ToBeat: " + data.strength + "</color>");
            if (rollResult > data.strength){ Debug.Log("<color=green>Success!</color>"); }
            else { Debug.Log("<color=red>Failure!</color>"); }
        }

        if (rollResult > data.strength){ return true; }

        return false;
    }
}

public enum Stance
{
    Safe,
    Aggressive,
    Focused
}