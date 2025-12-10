using System;
using TMPro;
using UnityEngine;

public class UI_DATA_SaveLoadUI : DEBUGMonoBehaviour
{
    public DATA_PLAYER_CallUpdatePlayerPersistantData dataUpdater;

    public TMP_InputField characterNameIF;

    public TextMeshProUGUI intCounter;
    public TextMeshProUGUI chaCounter;
    public TextMeshProUGUI strCounter;
    
    public TextMeshProUGUI ptsRemainingCounter;

    public void Save()
    {
        dataUpdater.SetCharacterName(characterNameIF.text);

        dataUpdater.SetIntelligence(float.Parse(intCounter.text));
        dataUpdater.SetCharisma(float.Parse(chaCounter.text));
        dataUpdater.SetStrength(float.Parse(strCounter.text));

        dataUpdater.Save();
    }

    public void Load()
    {
        characterNameIF.text = dataUpdater.GetCharacterName();

        intCounter.text = dataUpdater.GetIntelligence().ToString();
        chaCounter.text = dataUpdater.GetCharisma().ToString();
        strCounter.text = dataUpdater.GetStrength().ToString();

        UpdatePointsRemaining();
    }

    public void AmendIntCounter(int val)
    {
        if ((!QueryPointsRemaining()) && (val > 0)){ return; }

        int newInt = int.Parse(intCounter.text);

        if (newInt + val > RULES.RULE_STATS_maxIntelligence){ 
            newInt = RULES.RULE_STATS_maxIntelligence;
        }
        else if (newInt + val < 0){ newInt = 0; }
        else{ newInt += val; }

        intCounter.text = newInt.ToString();

        UpdatePointsRemaining();
    }
    public void AmendChaCounter(int val)
    {
        if ((!QueryPointsRemaining()) && (val > 0)){ return; }

        int newCha = int.Parse(chaCounter.text);

        if (newCha + val > RULES.RULE_STATS_maxCharisma){ 
            newCha = RULES.RULE_STATS_maxCharisma;
        }
        else if (newCha + val < 0){ newCha = 0; }
        else{ newCha += val; }

        chaCounter.text = newCha.ToString();

        UpdatePointsRemaining();
    }
    public void AmendStrCounter(int val)
    {
        if ((!QueryPointsRemaining()) && (val > 0)){ return; }

        int newStr = int.Parse(strCounter.text);

        if (newStr + val > RULES.RULE_STATS_maxStrength){ 
            newStr = RULES.RULE_STATS_maxStrength;
        }
        else if (newStr + val < 0){ newStr = 0; }
        else{ newStr += val; }

        strCounter.text = newStr.ToString();

        UpdatePointsRemaining();
    }

    public void UpdatePointsRemaining()
    {
        int intelligence = int.Parse(intCounter.text);
        int charisma = int.Parse(chaCounter.text);
        int strength = int.Parse(strCounter.text);

        ptsRemainingCounter.text = (RULES.RULE_STATS_statPoints - (intelligence + charisma + strength)).ToString();
    }

    public bool QueryPointsRemaining()
    {
        int intelligence = int.Parse(intCounter.text);
        int charisma = int.Parse(chaCounter.text);
        int strength = int.Parse(strCounter.text);

        if ((intelligence + charisma + strength) < RULES.RULE_STATS_statPoints) { return true; }

        return false;
    }
}