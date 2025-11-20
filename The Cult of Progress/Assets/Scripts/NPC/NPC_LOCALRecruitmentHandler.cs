using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class NPC_LOCALRecruitmentHandler : DEBUGMonoBehaviour
{
    [SerializeField]
    [OnValueChanged("UpdateAttemptsCounter")] //ONLY WORKS IN INSPECTOR
    private int conversionAttempts = RULES.RULE_CONVERSION_defaultAttempts;

    [Space(10)]

    [SerializeField]
    private NPC_RuntimeData data;

    [Space(10)]

    [SerializeField]
    private UnityEvent OnRecruitment;

    [SerializeField]
    private NPC_ConversionWindowHandler conversionWindowHandler;

    private void OnDataChange()
    {
        NPCSetup();
    }

    public void OnInteract()
    {
        conversionWindowHandler.Setup(this);
    }

    private void OnIntelligenceChange()
    {
        if (debug){Debug.Log("Intelligence Changed!");}
    }

    private void UpdateAttemptsCounter()
    {
        conversionWindowHandler.UpdateAttemptsCounter(this);
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

    public bool Convert(PLAYER_CharacterData playerData, Stance stance)
    {
        //Subtract attempt count
        if (conversionAttempts == 0)
        {
            return false;
        }
        --conversionAttempts;
        UpdateAttemptsCounter();

        int corruptionTotal = 0;
        int enthrallmentTotal = 0;
        int dominationTotal = 0;

        //Roll D6 per Stat Point for Player
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

        //Subtract RES
        if (corruptionTotal - data.RESCorruption <= 0){ corruptionTotal = 0; }
        else { corruptionTotal -= data.RESCorruption; }

        if (enthrallmentTotal - data.RESEnthrallment <= 0){ enthrallmentTotal = 0; }
        else { enthrallmentTotal -= data.RESEnthrallment; }

        if (dominationTotal - data.RESDomination <= 0){ data.RESDomination = 0; }
        else { dominationTotal -= data.RESDomination; }

        //Subtract Result from inn, det and will
        if (data.innocence - corruptionTotal <= 0){ data.innocence = 0; }
        else { data.innocence -= corruptionTotal; }

        if (data.detachment - enthrallmentTotal <= 0){ data.detachment = 0; }
        else { data.detachment -= enthrallmentTotal; }

        if (data.willpower - dominationTotal <= 0){ data.willpower = 0; }
        else { data.willpower -= dominationTotal; }

        //CheckConverted
        //Returns false if not converted
        CheckConverted();

        //Query was Performed???
        return true;
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
        if (debug){ Debug.Log("Recruited " + data.GetData().STAT_characterName + "!"); }

        OnRecruitment.Invoke();
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
            Debug.Log("Dice Roll Corruption: Roll: " + rollResult + " ToBeat: " + data.intelligence);
            if (rollResult > data.intelligence){ Debug.Log("Success!"); }
            else { Debug.Log("Failure!"); }
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
            Debug.Log("Dice Roll Enthrallment: Roll: " + rollResult + " ToBeat: " + data.charisma);
            if (rollResult > data.charisma){ Debug.Log("Success!"); }
            else { Debug.Log("Failure!"); }
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
            Debug.Log("Dice Roll Domination: Roll: " + rollResult + " ToBeat: " + data.strength);
            if (rollResult > data.strength){ Debug.Log("Success!"); }
            else { Debug.Log("Failure!"); }
        }

        if (rollResult > data.strength){ return true; }

        return false;
    }
}

public enum Stance
{
    None
}