using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class NPC_ConversionWindowHandler : DEBUGMonoBehaviour
{
    [SerializeField]
    private PLAYER_CharacterData playerData;
    [SerializeField]
    private TextMeshProUGUI attemptsCounter;
    [SerializeField]
    private TMP_Dropdown stanceDropDown;
    [SerializeField]
    private UnityEngine.UI.Button conversionButton;
    [SerializeField]
    private GameObject panel;

    [ShowIf("debug")]
    [SerializeField]
    private NPC_LOCALRecruitmentHandler targetNPCData;

    public void Awake()
    {
        if (targetNPCData == null){ ClosePanel(); }
        else{ Setup(targetNPCData); }
    }

    public void Setup(NPC_LOCALRecruitmentHandler newTargetNPCData)
    {
        targetNPCData = newTargetNPCData;

        UpdateAttemptsCounter();

        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void UpdateAttemptsCounter(NPC_LOCALRecruitmentHandler source = null)
    {
        if (debug){ Debug.Log("Attempting to Update Attempts Counter!"); }

        if ((source != null) && (source != targetNPCData)){ return; }

        int conversionAttempts = targetNPCData.GetConversionAttempts();

        if (conversionAttempts == 1){ attemptsCounter.text = "1 Attempt"; } 
        else { attemptsCounter.text = conversionAttempts + " Attempts"; }

        if (debug){ Debug.Log("Updated Attempts Counter to: " + attemptsCounter.text); }

        if (conversionAttempts == 0){ DisableInteractables(); }
        else { EnableInteractables(); }
    }

    public Stance GetStance()
    {
        string stanceChoice = stanceDropDown.options[stanceDropDown.value].text;

        return Stance.None;
    }

    public NPC_LOCALRecruitmentHandler GetHeldNPCData()
    {
        return targetNPCData;
    }

    public void Convert()
    {
        targetNPCData.Convert(playerData, GetStance());
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