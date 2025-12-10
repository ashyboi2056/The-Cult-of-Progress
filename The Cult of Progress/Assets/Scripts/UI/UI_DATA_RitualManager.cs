using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UI_DATA_RitualManager : DEBUGMonoBehaviour
{
    private List<RitualInstance> activeRituals = new List<RitualInstance>();
    public GameObject timerPrefab;
    public static UI_DATA_RitualManager instance;

    void Awake()
    {
        instance = this;
    }

    public static void COMMANDStartRitual(soDATA_RITUAL ritual)
    {
        instance.StartRitual(ritual);
    }

    public static void COMMANDInterruptRitual(soDATA_RITUAL ritual)
    {
        foreach (RitualInstance ritualInstance in instance.activeRituals)
        {
            if (ritualInstance.QueryIsRitual(ritual)){ ritualInstance.Interrupt(); }
        }
    }

    public RitualInstance StartRitual(soDATA_RITUAL ritual)
    {
        var instance = new RitualInstance(ritual, Instantiate(timerPrefab,transform));
        activeRituals.Add(instance);
        return instance;
    }

    private void Update()
    {
        for (int i = activeRituals.Count - 1; i >= 0; i--)
        {
            var ritual = activeRituals[i];
            ritual.Update(Time.deltaTime);
            if (ritual.IsFinished)
                activeRituals.RemoveAt(i);
        }
    }

    public static bool QueryStartRitual(soDATA_RITUAL ritual, NPC_IDEnums[] bound, soDATA_ITEM[] items)
    {
        if (ritual == null)
        {
            Debug.Log("CRITICAL ERROR: No Ritual Found at Query!");
            return false;
        }

        if (ritual.QueryStartRitual(bound, items)){ return true; }

        return false;
    }
}

public class RitualInstance
{
    private soDATA_RITUAL ritual;
    private float elapsed;
    public bool IsFinished { get; private set; }
    public GameObject linkedTimer;
    TextMeshProUGUI timerText;

    public RitualInstance(soDATA_RITUAL ritual, GameObject linkedTimer)
    {
        this.ritual = ritual;
        this.linkedTimer = linkedTimer;
        elapsed = 0f;

        timerText = linkedTimer.GetComponent<TextMeshProUGUI>();
    }

    public void Update(float deltaTime)
    {
        if (IsFinished) return;

        elapsed += deltaTime;
        if (elapsed >= ritual.STAT_ritualCompletionTime)
        {
            ritual.Performed();
            IsFinished = true;
            CleanUp();
        }

        UpdateTimer();
    }

    void UpdateTimer()
    {
        string timeToCompletion = Mathf.CeilToInt(RemainingTime).ToString();

        timerText.text = 
            "The Ritual: " 
            + 
            ritual.STAT_ritualName 
            + 
            " will be completed in: " + timeToCompletion;
    }

    void CleanUp()
    {
        GameObject.Destroy(linkedTimer);
    }

    public void Interrupt()
    {
        ritual.Interrupted();
        IsFinished = true;
        CleanUp();
    }

    public bool QueryIsRitual(soDATA_RITUAL ritual)
    {
        if (ritual == this.ritual){ return true; }
        return false;
    }

    public float RemainingTime => Mathf.Max(0, ritual.STAT_ritualCompletionTime - elapsed);
}