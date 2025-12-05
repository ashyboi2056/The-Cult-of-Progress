using UnityEngine;
using TMPro;
using NaughtyAttributes;
using System;

[ExecuteAlways]
public class UI_NPC_ConversionWindowHandler : DEBUGMonoBehaviour
{
    private PLAYER_CharacterData playerData;

    [SerializeField] private UI_Panel myUIPanelScript;

    [SerializeField] private TextMeshProUGUI nametag;
    [SerializeField] private TextMeshProUGUI attemptsCounter;
    [SerializeField] private TextMeshProUGUI innocenceCounter;
    [SerializeField] private TextMeshProUGUI detachmentCounter;
    [SerializeField] private TextMeshProUGUI willpowerCounter;
    [SerializeField] private TextMeshProUGUI resCorruptionCounter;
    [SerializeField] private TextMeshProUGUI resEnthrallmentCounter;
    [SerializeField] private TextMeshProUGUI resDominationCounter;
    [SerializeField]
    private TMP_Dropdown stanceDropDown;
    [SerializeField]
    private UnityEngine.UI.Button conversionButton;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject converted;

    [ShowIf("debug")]
    [SerializeField] private NPC_LOCALRecruitmentHandler targetNPCRecruitmentHandler;
    [ShowIf("debug")]
    [SerializeField] private NPC_RuntimeData targetNPCData;

    public void Awake()
    {
        if (playerData == null){ playerData = FindFirstObjectByType<PLAYER_CharacterData>(); }

        if (targetNPCRecruitmentHandler == null){ ClosePanel(); }
        else{ OpenPanel(); }
    }

    public void SetTargetNPC(NPC_LOCALRecruitmentHandler newTargetNPCRecruitmentHandler)
    {
        targetNPCRecruitmentHandler = newTargetNPCRecruitmentHandler;
        targetNPCData = targetNPCRecruitmentHandler.data;
    }

    private void Setup()
    {
        converted.SetActive(false);

        if (targetNPCData == null)
        { 
            if (debug){ Debug.Log("MAJOR ERROR: " + name + " was Setup without targetNPCData!"); }
            return;
        }
        if (targetNPCRecruitmentHandler == null)
        { 
            if (debug){ Debug.Log("MAJOR ERROR: " + name + " was Setup without targetNPCRecruitmentHandler!"); }
            return;
        }

        //Subscribe to OnChangeActions
        targetNPCData.OnInnocenceSetAct += UpdateStatsCounters;
        targetNPCData.OnDetachmentSetAct += UpdateStatsCounters;
        targetNPCData.OnWillpowerSetAct += UpdateStatsCounters;

        targetNPCData.OnRESCorruptionSetAct += UpdateStatsCounters;
        targetNPCData.OnRESEnthrallmentSetAct += UpdateStatsCounters;
        targetNPCData.OnRESDominationSetAct += UpdateStatsCounters;

        UpdateCounters();

        nametag.text = targetNPCData.GetData().STAT_characterName;
    }

    private void Setdown()
    {
        //Unsubscribe from OnChangeActions

        if (targetNPCData != null)
        {
            targetNPCData.OnInnocenceSetAct -= UpdateStatsCounters;
            targetNPCData.OnDetachmentSetAct -= UpdateStatsCounters;
            targetNPCData.OnWillpowerSetAct -= UpdateStatsCounters;

            targetNPCData.OnRESCorruptionSetAct -= UpdateStatsCounters;
            targetNPCData.OnRESEnthrallmentSetAct -= UpdateStatsCounters;
            targetNPCData.OnRESDominationSetAct -= UpdateStatsCounters;
        }

        if (targetNPCRecruitmentHandler != null)
        {
            targetNPCRecruitmentHandler = null;
        }

        nametag.text = "NULL";
    }

    public void OpenPanel()
    {
        if (debug){ Debug.Log("Opening Panel at: " + name); }

        Setup();

        panel.SetActive(true);

        myUIPanelScript.isPanelOpen = true;
    }

    public void ClosePanel()
    {
        if (debug){ Debug.Log("Closing Panel at: " + name); }

        panel.SetActive(false);

        Setdown();

        myUIPanelScript.isPanelOpen = false;
    }

    public void Converted()
    {
        converted.SetActive(true);
    }

    private void UpdateCounters()
    {
        UpdateAttemptsCounter(targetNPCRecruitmentHandler);
        UpdateStatsCounters();
    }

    public void UpdateAttemptsCounter(NPC_LOCALRecruitmentHandler source = null)
    {
        if (debug){ Debug.Log("Attempting to Update Attempts Counter!"); }

        if ((source != null) && (source != targetNPCRecruitmentHandler)){ return; }

        int conversionAttempts = targetNPCRecruitmentHandler.GetConversionAttempts();

        if (conversionAttempts == 1){ attemptsCounter.text = "1 Attempt"; } 
        else { attemptsCounter.text = conversionAttempts + " Attempts"; }

        if (debug){ Debug.Log("Updated Attempts Counter to: " + attemptsCounter.text); }

        if (conversionAttempts == 0){ DisableInteractables(); }
        else { EnableInteractables(); }
    }

    private void UpdateStatsCounters()
    {
        if (targetNPCRecruitmentHandler == null){ return; }

        innocenceCounter.text = targetNPCData.innocence.ToString() + " Innocence";
        detachmentCounter.text = targetNPCData.detachment.ToString() + " Detachment";
        willpowerCounter.text = targetNPCData.willpower.ToString() + " Willpower";

        resCorruptionCounter.text = targetNPCData.RESCorruption.ToString() + " RESCorruption";
        resEnthrallmentCounter.text = targetNPCData.RESEnthrallment.ToString() + " RESEnthrallment";
        resDominationCounter.text = targetNPCData.RESDomination.ToString() + " RESDomination";
    }

    public Stance GetStance()
    {
        string stanceChoice = stanceDropDown.options[stanceDropDown.value].text;

        return (Stance)Enum.Parse(typeof(Stance), stanceChoice);
    }

    public NPC_LOCALRecruitmentHandler GetHeldNPCData()
    {
        return targetNPCRecruitmentHandler;
    }

    public void Convert()
    {
        StartCoroutine(targetNPCRecruitmentHandler.Convert(playerData, GetStance()));
    }

    private void EnableInteractables()
    {
        stanceDropDown.interactable = true;
        conversionButton.interactable = true;
    }

    private void DisableInteractables()
    {
        stanceDropDown.interactable = false;
        conversionButton.interactable = false;
    }
}