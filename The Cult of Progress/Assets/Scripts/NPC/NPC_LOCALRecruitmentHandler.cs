using NaughtyAttributes;
using UnityEngine;

public class NPC_LOCALRecruitmentHandler : DEBUGMonoBehaviour
{
    // LOCAL DATA ONLY //
    // SYNCING DATA ACROSS NETWORK MEANS PLAYERS CAN BENEFIT FROM OPPONENTS CORRUPTION ATTEMPTS //

    [SerializeField]
    [Expandable]
    [OnValueChanged("OnDataChange")]
    private NPC_Stats data;

    [Space(10)]

    [SerializeField]
    private int willpower;
    [SerializeField]
    private int devotionlessness;
    [SerializeField]
    private int innocence;

    [Space(10)]

    [SerializeField]
    private int RESDomination;
    [SerializeField]
    private int RESEnthrallment;
    [SerializeField]
    private int RESCorruption;

    private void OnDataChange()
    {
        NPCSetup();
    }

    [Button("Reset NPC Stats")]
    private void NPCSetup()
    {
        willpower = data.STAT_willpower;
        devotionlessness = data.STAT_devotionlessness;
        innocence = data.STAT_innocence;

        RESDomination = data.STAT_startRESDomination;
        RESEnthrallment = data.STAT_startRESEnthrallment;
        RESCorruption = data.STAT_startRESCorruption;
    }
}