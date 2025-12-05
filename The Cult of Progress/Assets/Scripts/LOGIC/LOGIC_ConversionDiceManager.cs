using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LOGIC_ConversionDiceManager : DEBUGMonoBehaviour
{
    public static LOGIC_ConversionDiceManager instance;

    //public static int 

    public GameObject intDicePrefab;
    public GameObject chaDicePrefab;
    public GameObject strDicePrefab;

    public Transform defaultDiceSpawn;

    public static int intResults;
    public static int chaResults;
    public static int strResults;

    public bool resultsAvailable = false;

    public List<LOGIC_ConversionDice> diceList = new List<LOGIC_ConversionDice>();

    void Awake(){ instance = this; }

    public static IEnumerator RollCoroutine(
        int intDice = 0, int chaDice = 0, int strDice = 0,
        int intToBeat = 6, int chaToBeat = 6, int strToBeat = 6,
        System.Action<List<int>> onComplete = null
    )
    {
        if (instance.debug){ Debug.Log("Starting Roll Coroutine!"); }
        instance.resultsAvailable = false;

        // Spawn dice
        instance.SpawnDice(intDice, chaDice, strDice, intToBeat, chaToBeat, strToBeat);

        // Wait until all dice have settled
        yield return new WaitUntil(() => instance.resultsAvailable);

        // Collect results
        int corruptionTotal = intResults;
        int enthrallmentTotal = chaResults;
        int dominationTotal = strResults;

        var results = new List<int> { corruptionTotal, enthrallmentTotal, dominationTotal };

        // Return via callback
        onComplete?.Invoke(results);
    }

    public void SpawnDice(int intDice = 0, int chaDice = 0, int strDice = 0, int intToBeat = 6, int chaToBeat = 6, int strToBeat = 6)
    {
        foreach (LOGIC_ConversionDice die in diceList) {
            Destroy(die.gameObject); 
        }
        diceList.Clear();
        Debug.Log(diceList);

        for (int i = intDice; i > 0; i--){ SpawnIntDice(intToBeat); }
        for (int i = chaDice; i > 0; i--){ SpawnChaDice(chaToBeat); }
        for (int i = strDice; i > 0; i--){ SpawnStrDice(strToBeat); }
    }

    public void OnDieSettled(LOGIC_ConversionDice die) {
        if (AllDiceSettled()) {
            StartCoroutine(GatherResultsAfterDelay());
        }
    }

    bool AllDiceSettled() {
        foreach (LOGIC_ConversionDice die in diceList) {
            if (!die.hasSettled) return false;
        }
        return true;
    }

    IEnumerator GatherResultsAfterDelay() {
        yield return new WaitForSeconds(0.5f + RULES.RULE_UX_diceResultDelay); // small buffer + Delay for effect
        GatherResults();
    }

    public void GatherResults() {
        intResults = 0;
        chaResults = 0;
        strResults = 0;

        foreach (LOGIC_ConversionDice die in diceList) {
            switch (die.type)
            {
                case DiceType.Int:
                    if (die.CalculateResult()){ intResults ++; }
                    break;
                case DiceType.Cha:
                    if (die.CalculateResult()){ chaResults ++; }
                    break;
                case DiceType.Str:
                    if (die.CalculateResult()){ strResults ++; }
                    break;
            }
        }

        if (debug){ Debug.Log("Results Gathered!"); }
        if (debug){ Debug.Log("Int: " + intResults); }
        if (debug){ Debug.Log("Cha: " + chaResults); }
        if (debug){ Debug.Log("Str: " + strResults); }

        resultsAvailable = true;
    }


    private void SpawnIntDice(int toBeat = 6){ 
        if (debug){ Debug.Log("Spawning Int Die!"); }

        GameObject newDice = Instantiate(intDicePrefab, defaultDiceSpawn); 
        newDice.GetComponent<LOGIC_ConversionDice>().toBeat = toBeat;

        diceList.Add(newDice.GetComponent<LOGIC_ConversionDice>());
    }
    private void SpawnChaDice(int toBeat = 6){ 
        if (debug){ Debug.Log("Spawning Cha Die!"); }

        GameObject newDice = Instantiate(chaDicePrefab, defaultDiceSpawn); 
        newDice.GetComponent<LOGIC_ConversionDice>().toBeat = toBeat;

        diceList.Add(newDice.GetComponent<LOGIC_ConversionDice>());
    }
    private void SpawnStrDice(int toBeat = 6){ 
        if (debug){ Debug.Log("Spawning Str Die!"); }
        
        GameObject newDice = Instantiate(strDicePrefab, defaultDiceSpawn); 
        newDice.GetComponent<LOGIC_ConversionDice>().toBeat = toBeat;

        diceList.Add(newDice.GetComponent<LOGIC_ConversionDice>());
    }

}